using Linql.Client.Internal;
using Linql.Client.Test.TestFiles;

namespace Linql.Client.Test
{
    public class SmokeTest : TestFileTests
    {
        protected LinqlContext Context { get; set; } = new LinqlContext();

        [Test]
        //Should fail.
        public void IncorrectToJson()
        {
            List<int> listType = new List<int>();

            try
            {
                listType.AsQueryable().ToJson();
            }
            catch(UnsupportedIQueryableException ex)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }

        [Test]
        //Should fail.
        public async Task IncorrectToJsonAsync()
        {
            List<int> listType = new List<int>();

            try
            {
                await listType.AsQueryable().ToJsonAsync();
            }
            catch (UnsupportedIQueryableException ex)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }


        [Test]
        public void EmptySearch()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string empty = search.ToJson();

            this.TestLoader.Compare(nameof(SmokeTest.EmptySearch), empty);
        }

        [Test]
        public async Task SimpleConstant()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => true).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.SimpleConstant), simpleConstant);
        }

        [Test]
        public async Task SimpleBooleanProperty()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.Boolean).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.SimpleBooleanProperty), simpleConstant);
        }

        [Test]
        public async Task BooleanNegate()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => !r.Boolean).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.BooleanNegate), simpleConstant);
        }

        [Test]
        public async Task SimpleBooleanPropertyChaining()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.OneToOne.Boolean).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.SimpleBooleanPropertyChaining), simpleConstant);
        }
        
        [Test]
        public async Task SimpleBooleanPropertyEquals()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.Boolean == false).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.SimpleBooleanPropertyEquals), simpleConstant);
        }

        [Test]
        public async Task SimpleBooleanPropertyEqualsSwap()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => false == r.Boolean).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.SimpleBooleanPropertyEqualsSwap), simpleConstant);
        }

        [Test]
        public async Task TwoBooleans()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => false == true).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.TwoBooleans), simpleConstant);
        }

        [Test]
        public async Task BooleanVar()
        {
            bool test = false;
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => false == test).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.BooleanVar), simpleConstant);
        }

        [Test]
        public async Task ComplexBoolean()
        {
            DataModel test = new DataModel();
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => test.Boolean).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.ComplexBoolean), simpleConstant);
        }

        [Test]
        public async Task ComplexBooleanAsArgument()
        {
            DataModel test = new DataModel();
            await this.InternalComplexBooleanAsArgument(test);
         
        }

        private async Task InternalComplexBooleanAsArgument(DataModel test)
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => test.Boolean).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.ComplexBoolean), simpleConstant);
        }

        [Test]
        public async Task ThreeBooleans()
        {
            DataModel test = new DataModel();
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.Boolean && r.Boolean && r.Boolean).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.ThreeBooleans), simpleConstant);
        }

        [Test]
        public async Task ListInt()
        {
            List<int> integers = new List<int>() { 1, 2, 3 };
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => integers.Contains(r.Integer)).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.ListInt), simpleConstant);
        }


        [Test]
        public async Task ListIntFromProperty()
        {
            List<int> integers = new List<int>() { 1, 2, 3 };
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.ListInteger.Contains(1)).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.ListIntFromProperty), simpleConstant);
        }

        [Test]
        public async Task InnerLambda()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.ListInteger.Any(s => s ==1)).ToJsonAsync();
            this.TestLoader.Compare(nameof(SmokeTest.InnerLambda), simpleConstant);
        }



    }

}