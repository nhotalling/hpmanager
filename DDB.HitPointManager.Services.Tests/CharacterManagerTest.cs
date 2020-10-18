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
    }
}