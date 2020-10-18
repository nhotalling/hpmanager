using System.Collections.Generic;
using DDB.HitPointManager.Domain;
using Moq;
using NUnit.Framework;

namespace DDB.HitPointManager.Services.Tests
{
    [TestFixture]
    public class CharacterManagerTest
    {
        private ICharacterManager _subject;
        private Mock<ICharacterService> _mockCharacterService;
        private Mock<ICharacterHealthService> _mockCharacterHealthService;

        [SetUp]
        public void Setup()
        {
            _mockCharacterService = new Mock<ICharacterService>();
            _mockCharacterHealthService = new Mock<ICharacterHealthService>();

            _subject = new CharacterManager(_mockCharacterService.Object,
                                            _mockCharacterHealthService.Object);
        }

        [Test]
        public void AddTempHp_SubtractsIfNegative()
        {
            _mockCharacterHealthService.Setup(service =>
                    service.GetCharacterHealth(It.IsAny<string>()))
                .Returns(new CharacterHealth{TempHp = 10});

            var result = _subject.AddTempHp("Briv", -2);

            Assert.AreEqual(8, result.TempHp);
        }

        [Test]
        public void AddTempHp_AddsInitialAmount()
        {
            _mockCharacterHealthService.Setup(service =>
                    service.GetCharacterHealth(It.IsAny<string>()))
                .Returns(new CharacterHealth { TempHp = 0 });

            var result = _subject.AddTempHp("Briv", 2);

            Assert.AreEqual(2, result.TempHp);
        }

        [Test]
        public void AddTempHp_KeepsExisting()
        {
            _mockCharacterHealthService.Setup(service =>
                    service.GetCharacterHealth(It.IsAny<string>()))
                .Returns(new CharacterHealth { TempHp = 10 });

            var result = _subject.AddTempHp("Briv", 2);

            Assert.AreEqual(10, result.TempHp);
        }

        [Test]
        public void AddTempHp_OverwritesExisting()
        {
            _mockCharacterHealthService.Setup(service =>
                    service.GetCharacterHealth(It.IsAny<string>()))
                .Returns(new CharacterHealth { TempHp = 2 });

            var result = _subject.AddTempHp("Briv", 10);

            Assert.AreEqual(10, result.TempHp);
        }

        [Test]
        public void Heal_IncreasesCurrentHp()
        {
            _mockCharacterHealthService.Setup(service =>
                    service.GetCharacterHealth(It.IsAny<string>()))
                .Returns(new CharacterHealth
                {
                    CurrentHp = 2,
                    MaxHp = 25
                });

            var result = _subject.Heal("Briv", 10);

            Assert.AreEqual(12, result.CurrentHp);
        }

        [Test]
        public void Heal_DoesNotExceedMaxHp()
        {
            _mockCharacterHealthService.Setup(service =>
                    service.GetCharacterHealth(It.IsAny<string>()))
                .Returns(new CharacterHealth
                {
                    CurrentHp = 2,
                    MaxHp = 25
                });

            var result = _subject.Heal("Briv", 1000);

            Assert.AreEqual(25, result.CurrentHp);
        }

        [Test]
        public void Heal_ThrowsForNegativeValues()
        {
            _mockCharacterHealthService.Setup(service =>
                    service.GetCharacterHealth(It.IsAny<string>()))
                .Returns(new CharacterHealth
                {
                    CurrentHp = 2,
                    MaxHp = 25
                });

            Assert.That(() => _subject.Heal("Briv", -10), Throws.ArgumentException);
        }

        // Saves if > 0
        // Does not save if 0

        [Test]
        public void CalculateDamage_AppliesImmunity()
        {
            var damage = new List<DamageRequest>
            {
                new DamageRequest
                {
                    Type = DamageType.Cold,
                    Value = 6
                },
                new DamageRequest
                {
                    Type = DamageType.Slashing,
                    Value = 10
                }
            };
            var defense = new List<Defense>
            {
                new Defense
                {
                    DefenseType = DefenseType.Immunity,
                    Type = DamageType.Slashing
                }
            };
            var result = _subject.CalculateDamage(damage, defense);
            Assert.AreEqual(6, result);
        }

        [Test]
        public void CalculateDamage_DoesNotApplyImmunity()
        {
            var damage = new List<DamageRequest>
            {
                new DamageRequest
                {
                    Type = DamageType.Cold,
                    Value = 6
                },
                new DamageRequest
                {
                    Type = DamageType.Slashing,
                    Value = 10
                }
            };
            var defense = new List<Defense>
            {
                new Defense
                {
                    DefenseType = DefenseType.Immunity,
                    Type = DamageType.Acid
                }
            };
            var result = _subject.CalculateDamage(damage, defense);
            Assert.AreEqual(16, result);
        }

        [Test]
        public void CalculateDamage_AppliesVulnerability()
        {
            var damage = new List<DamageRequest>
            {
                new DamageRequest
                {
                    Type = DamageType.Cold,
                    Value = 6
                },
                new DamageRequest
                {
                    Type = DamageType.Slashing,
                    Value = 10
                }
            };
            var defense = new List<Defense>
            {
                new Defense
                {
                    DefenseType = DefenseType.Vulnerability,
                    Type = DamageType.Cold
                }
            };
            var result = _subject.CalculateDamage(damage, defense);
            Assert.AreEqual(22, result);
        }

        [Test]
        public void CalculateDamage_DoesNotApplyVulnerability()
        {
            var damage = new List<DamageRequest>
            {
                new DamageRequest
                {
                    Type = DamageType.Cold,
                    Value = 6
                },
                new DamageRequest
                {
                    Type = DamageType.Slashing,
                    Value = 10
                }
            };
            var defense = new List<Defense>
            {
                new Defense
                {
                    DefenseType = DefenseType.Vulnerability,
                    Type = DamageType.Thunder
                }
            };
            var result = _subject.CalculateDamage(damage, defense);
            Assert.AreEqual(16, result);
        }

        [Test]
        public void CalculateDamage_AppliesResistance()
        {
            var damage = new List<DamageRequest>
            {
                new DamageRequest
                {
                    Type = DamageType.Cold,
                    Value = 7
                },
                new DamageRequest
                {
                    Type = DamageType.Slashing,
                    Value = 10
                }
            };
            var defense = new List<Defense>
            {
                new Defense
                {
                    DefenseType = DefenseType.Resistance,
                    Type = DamageType.Cold
                }
            };
            var result = _subject.CalculateDamage(damage, defense);
            Assert.AreEqual(13, result);
        }

        [Test]
        public void CalculateDamage_DoesNotApplyResistance()
        {
            var damage = new List<DamageRequest>
            {
                new DamageRequest
                {
                    Type = DamageType.Cold,
                    Value = 6
                },
                new DamageRequest
                {
                    Type = DamageType.Slashing,
                    Value = 10
                }
            };
            var defense = new List<Defense>
            {
                new Defense
                {
                    DefenseType = DefenseType.Resistance,
                    Type = DamageType.Thunder
                }
            };
            var result = _subject.CalculateDamage(damage, defense);
            Assert.AreEqual(16, result);
        }

        [Test]
        public void CalculateDamage_AttacksBrivWithFlameTongue()
        {
            var damage = new List<DamageRequest>
            {
                new DamageRequest
                {
                    Type = DamageType.Slashing,
                    Value = 9
                },
                new DamageRequest
                {
                    Type = DamageType.Fire,
                    Value = 6
                }
            };
            var defense = new List<Defense>
            {
                new Defense
                {
                    DefenseType = DefenseType.Immunity,
                    Type = DamageType.Fire
                },
                new Defense
                {
                    DefenseType = DefenseType.Resistance,
                    Type = DamageType.Slashing
                }
            };
            var result = _subject.CalculateDamage(damage, defense);
            Assert.AreEqual(4, result);
        }
    }
}