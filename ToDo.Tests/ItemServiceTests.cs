using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Ploeh.AutoFixture;
using ToDo.Business;
using ToDo.Interfaces.Data;
using ToDo.Models;

namespace ToDo.Tests
{
    [TestFixture]
    public class ItemServiceTests
    {
        [Test]
        public void Statistics_TotalCountTest()
        {
            Fixture fixture = new Fixture();
            var testItems = 
                 fixture.Build<Item>()
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(10).AsQueryable();

            //arrange
            Mock<IItemRepository> repoMock = new Mock<IItemRepository>();
            repoMock.Setup(r => r.GetItems())
                .Returns(testItems);

            var sut = new ItemService(repoMock.Object);
            //act

            var result = sut.GetStatistics();
            
            //assert
            Assert.That(result.TotalCount == 10);
        }

        [Test]
        public void BigTaskCountTest()
        {
            Fixture fixture = new Fixture();

            var testItemsBTask = fixture.Build<Item>()
                .With(i => i.Hours, 101)
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(3);

            var testItemsMTask = fixture.Build<Item>()
                .With(i => i.Hours, 50)
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(5);

            var testItemsSTask = fixture.Build<Item>()
                .With(i => i.Hours, 24)
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(7);

            var testitems = (testItemsBTask.Concat(testItemsMTask).Concat(testItemsSTask)).AsQueryable();
            //arrange
            Mock<IItemRepository> repoMock = new Mock<IItemRepository>();
            repoMock.Setup(r => r.GetItems())
                .Returns(testitems);

            var sut = new ItemService(repoMock.Object);
            //act

            var result = sut.GetStatistics();

            //assert
            Assert.That(result.BigTaskCount == 3);
        }

        [Test]
        public void SmallTaskCountTest()
        {
            Fixture fixture = new Fixture();

            var testItemsBigTask = fixture.Build<Item>()
                .With(i => i.Hours, 101)
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(3);

            var testItemsMiddleTask = fixture.Build<Item>()
                .With(i => i.Hours, 50)
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(5);

            var testItemsSmallTask = fixture.Build<Item>()
                .With(i => i.Hours, 24)
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(7);

            var testitems = (testItemsBigTask.Concat(testItemsMiddleTask).Concat(testItemsSmallTask)).AsQueryable();
            //arrange
            Mock<IItemRepository> repoMock = new Mock<IItemRepository>();
            repoMock.Setup(r => r.GetItems())
                .Returns(testitems);

            var sut = new ItemService(repoMock.Object);
            //act

            var result = sut.GetStatistics();

            //assert
            Assert.That(result.SmallTaskCount == 7);
        }

        [Test]
        public void AvarageHoursCountTest()
        {
            Fixture fixture = new Fixture();

            var testItemsA = fixture.Build<Item>()
                .With(i => i.Hours, 25)
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(3);

            var testItemsB = fixture.Build<Item>()
                .With(i => i.Hours, 23)
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(1);

            var testItemsC = fixture.Build<Item>()
                .With(i => i.Hours, 11)
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(2);

            var testitems = (testItemsA.Concat(testItemsB).Concat(testItemsC)).AsQueryable();
            //arrange
            Mock<IItemRepository> repoMock = new Mock<IItemRepository>();
            repoMock.Setup(r => r.GetItems())
                .Returns(testitems);

            var sut = new ItemService(repoMock.Object);
            //act

            var result = sut.GetStatistics();

            //assert
            Assert.That(result.AvarageHours == 20);
        }


        [Test]
        public void PeopleCountTest()
        {
            Fixture fixture = new Fixture();

            var testItemsKeesje = fixture.Build<Item>()
            .With(i => i.Owner, "Keesje")
            .With(i => i.DueDate, DateTime.Now)
            .CreateMany(2);

            var testItemBartje = fixture.Build<Item>()
            .With(i => i.Owner, "Bartje")
            .With(i => i.DueDate, DateTime.Now)
            .CreateMany(1);

            var testItemsPietje = fixture.Build<Item>()
            .With(i => i.Owner, "Pietje")
            .With(i => i.DueDate, DateTime.Now)
            .CreateMany(3);

            var testitems = (testItemsKeesje.Concat(testItemBartje).Concat(testItemsPietje)).AsQueryable();
 
            //arrange
            Mock<IItemRepository> repoMock = new Mock<IItemRepository>();
            repoMock.Setup(r => r.GetItems())
                .Returns(testitems);

            var sut = new ItemService(repoMock.Object);
            //act
            var result = sut.GetStatistics();

            //assert
            Assert.That(result.PeopleCount == 3);
        }

        [Test]
        public void AverageHoursPerPersonCountTest()
        {
            Fixture fixture = new Fixture();

            var testItemsHarry = fixture.Build<Item>()
            .With(i => i.Hours, 32)
            .With(i => i.Owner, "Harry")
            .With(i => i.DueDate, DateTime.Now)
            .CreateMany(2);

            var testItemBertje = fixture.Build<Item>()
                .With(i => i.Hours, 23)
                .With(i => i.Owner, "Bertje")
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(1);

            var testItemsKlaasje = fixture.Build<Item>()
                .With(i => i.Hours, 11)
                .With(i => i.Owner, "Klaasje")
                .With(i => i.DueDate, DateTime.Now)
                .CreateMany(3);

            var testitems = (testItemsHarry.Concat(testItemBertje).Concat(testItemsKlaasje)).AsQueryable();

            //arrange
            Mock<IItemRepository> repoMock = new Mock<IItemRepository>();
            repoMock.Setup(r => r.GetItems())
                .Returns(testitems);

            var sut = new ItemService(repoMock.Object);
            //act
            var result = sut.GetStatistics();

            //assert
            Assert.That(result.AvarageHoursPerPerson == 40);
        }

        [Test]
        public void NextMonthStatisticsTest_CountsFutureMainTables()
        {
            Fixture fixture = new Fixture();

            var testitemsNextMonth = fixture.Build<Item>()
            .With(i => i.DueDate, DateTime.Now.AddDays(-1))
            .CreateMany(2).AsQueryable();

            var testitemsLastMonth = fixture.Build<Item>()
            .With(i => i.DueDate, DateTime.Now.AddDays(3))
            .CreateMany(5).AsQueryable();

            var testitems = (testitemsNextMonth.Concat(testitemsLastMonth)).AsQueryable();
            //arrange
            Mock<IItemRepository> repoMock = new Mock<IItemRepository>();
            repoMock.Setup(r => r.GetItems())
                .Returns(testitems);

            var sut = new ItemService(repoMock.Object);
            //act
            var result = sut.GetStatistics();

            //assert
            Assert.That(result.NextMonthStatistic.Count == 3);
        }

    }
}
