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
    }
}