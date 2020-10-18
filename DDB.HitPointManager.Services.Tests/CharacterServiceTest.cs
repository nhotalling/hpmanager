using System.Collections.Generic;
using DDB.HitPointManager.Core;
using DDB.HitPointManager.Data;
using DDB.HitPointManager.Domain;
using Moq;
using NUnit.Framework;

namespace DDB.HitPointManager.Services.Tests
{
    [TestFixture]
    public class CharacterServiceTest : MockData
    {
        private ICharacterService _subject;
        private Mock<ICharacterRepository> _mockCharacterRepository;

        [SetUp]
        public void Setup()
        {
            _mockCharacterRepository = new Mock<ICharacterRepository>();
            _subject = new CharacterService(_mockCharacterRepository.Object);
        }

        [Test]
        public void CalculateMaxHp_RequiresNonNullCharacter()
        {
            Assert.That(() => _subject.CalculateMaxHp(null), Throws.ArgumentException);
        }

        [Test]
        public void CalculateMaxHp_RequiresNonNullCharacterClasses()
        {
            var character = new Character();    
            Assert.That(() => _subject.CalculateMaxHp(character), Throws.ArgumentException);
        }

        [Test]
        public void CalculateMaxHp_RequiresAtLeastOneCharacterClass()
        {
            var character = new Character
            {
                Classes = new List<CharacterClass>()
            };
            Assert.That(() => _subject.CalculateMaxHp(character), Throws.ArgumentException);
        }

        [Test]
        public void CalculateMaxHp_CalculatesBrivCorrectly()
        {
            var character = GetTestCharacter("Briv");
            var maxHp = _subject.CalculateMaxHp(character);
            Assert.AreEqual(45, maxHp);
        }

        [Test]
        public void CalculateStatBonus_RequiresNonNullStats()
        {
            var character = new Character();
            Assert.That(() => _subject.CalculateStatBonus(character, StatType.Constitution), Throws.ArgumentException);
        }

        [Test]
        public void CalculateStatBonus_DoesNotRequireItems()
        {
            var character = new Character
            {
                Stats = new Stats
                {
                    Constitution = 14
                }
            };
            var bonus = _subject.CalculateStatBonus(character, StatType.Constitution);
            Assert.AreEqual(2, bonus);
        }

        [Test]
        public void CalculateStatBonus_CalculatesBrivCorrectly()
        {
            // Briv has CON 14 with +2. CON 16 should have +3 bonus.
            var character = GetTestCharacter("Briv");
            var bonus = _subject.CalculateStatBonus(character, StatType.Constitution);
            Assert.AreEqual(3, bonus);
        }

        [Test]
        public void CalculateStatBonus_StacksBonuses()
        {
            var character = new Character
            {
                Stats = new Stats
                {
                    Constitution = 15
                },
                Items = new List<Item>
                {
                    new Item
                    {
                        Modifier = new Modifier
                        {
                            AffectedObject = Constants.Stats,
                            AffectedValue = Constants.Constitution,
                            Value = 4
                        }
                    },
                    new Item
                    {
                        Modifier = new Modifier
                        {
                            AffectedObject = Constants.Stats,
                            AffectedValue = Constants.Constitution,
                            Value = 2
                        }
                    },
                    new Item
                    {
                        Modifier = new Modifier
                        {
                            AffectedObject = Constants.Stats,
                            AffectedValue = Constants.Constitution,
                            Value = -1
                        }
                    },
                    new Item
                    {
                        Modifier = new Modifier
                        {
                            AffectedObject = Constants.Stats,
                            AffectedValue = Constants.Constitution,
                            Value = 5
                        }
                    },
                }
            };
            var bonus = _subject.CalculateStatBonus(character, StatType.Constitution);
            Assert.AreEqual(7, bonus);
        }

        [Test]
        public void CalculateStatBonus_UsesCorrectStat()
        {
            var character = new Character
            {
                Stats = new Stats
                {
                    Constitution = 14
                },
                Items = new List<Item>
                {
                    new Item
                    {
                        Modifier = new Modifier
                        {
                            AffectedObject = Constants.Stats,
                            AffectedValue = Constants.Wisdom,
                            Value = 4
                        }
                    },
                    new Item
                    {
                        Modifier = new Modifier
                        {
                            AffectedObject = Constants.Stats,
                            AffectedValue = Constants.Strength,
                            Value = 10
                        }
                    },
                    new Item
                    {
                        Modifier = new Modifier
                        {
                            AffectedObject = Constants.Stats,
                            AffectedValue = Constants.Constitution,
                            Value = -2
                        }
                    },
                    new Item
                    {
                        Modifier = new Modifier
                        {
                            AffectedObject = Constants.Stats,
                            AffectedValue = Constants.Intelligence,
                            Value = 4
                        }
                    },
                }
            };
            var bonus = _subject.CalculateStatBonus(character, StatType.Constitution);
            Assert.AreEqual(1, bonus);
        }
    }
}