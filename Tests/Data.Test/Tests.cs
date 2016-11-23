using System;
using System.Linq;
using Xunit;
using Bzway.Data.Core;

namespace Tests
{
    public class Tests
    {
        public void TestDateTime()
        {
            DateTime baseTime = new DateTime(1970, 1, 1);
            var expected = (long)(DateTime.UtcNow - baseTime).TotalSeconds;
            var v1 = DateTime.Now.ToFileTimeUtc();
            var v2 = DateTime.UtcNow.ToFileTime();
            Assert.Equal(expected, v1);
            Assert.Equal(expected, v2);
        }

        [Fact]
        public void Test1()
        {
            using (var db = OpenDatabase.GetDatabase("SQLServer"))
            {
                db.Entity<test>().Insert(new test() { Name = "test", UUID = "test" });
                var entity = db.Entity<test>().Query().Where(m => m.UUID, "test", CompareType.Equal).First();
                Assert.Null(entity);
                db.Entity<test>().Update(new test() { Name = "updated", UUID = "test" });
                entity = db.Entity<test>().Query().Where(m => m.Name, "updated", CompareType.Equal).First();
                Assert.NotNull(entity);
                db.Entity<test>().Delete("test");
                entity = db.Entity<test>().Query().Where(m => m.Name, "updated", CompareType.Equal).First();
                Assert.Null(entity);
                var list = db.Entity<test>().Query().ToList();
            }
            Assert.True(true);
        }
    }
    public class test : EntityBase
    {
        public string UUID { get; set; }
        public string Name { get; set; }

    }
}
