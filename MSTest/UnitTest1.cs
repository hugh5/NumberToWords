using Microsoft.Playwright;

namespace MSTest
{
    [TestClass]
    public class UnitTest1 : PageTest
    {
        private ILocator Input { get; set; }
        private ILocator SubmitButton { get; set; }
        private ILocator Result { get; set; }

        [TestInitialize]
        public async Task TestInitialize()
        {
            await Page.GotoAsync("http://localhost:5000/");
            Input = Page.Locator("#numberInput");
            SubmitButton = Page.Locator("button[type='submit']");
            Result = Page.Locator("#result");
            Assert.IsNotNull(Input);
            Assert.IsNotNull(SubmitButton);
            Assert.IsNotNull(Result);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await Page.CloseAsync();
        }

        [TestMethod]
        public async Task HasCorrectTitle()
        {
            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync(new Regex("Number to Words Converter"));
        }

        [TestMethod]
        public async Task TestExample()
        {
            // Type a number into the input field
            await Input.FillAsync("123.45");
            await SubmitButton.ClickAsync();
            // Checking if it contains the expected word
            await Expect(Result).ToHaveTextAsync(new Regex("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS"));
        }

        [TestMethod]
        public async Task TestZero()
        {
            await Input.FillAsync("0");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("ZERO DOLLARS"));
        }

        [TestMethod]
        public async Task TestOneCent()
        {
            await Input.FillAsync("0.01");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("ONE CENT"));
        }

        [TestMethod]
        public async Task TestOneDollarOneCent()
        {
            await Input.FillAsync("1.01");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("ONE DOLLAR AND ONE CENT"));
        }

        [TestMethod]
        public async Task TestRandomBinary()
        {
            await Input.FillAsync("11101100001000010010100110.11");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("ELEVEN SEPTILLION, ONE HUNDRED AND ONE SEXTILLION, ONE HUNDRED QUINTILLION, ONE QUADRILLION, TEN BILLION, TEN MILLION, ONE HUNDRED THOUSAND, ONE HUNDRED AND TEN DOLLARS AND ELEVEN CENTS"));
        }

        [TestMethod]
        public async Task TestMultipleValidInput()
        {
            await Input.FillAsync("2300934.05");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("TWO MILLION, THREE HUNDRED THOUSAND, NINE HUNDRED AND THIRTY-FOUR DOLLARS AND FIVE CENTS"));

            await Input.FillAsync("83491275.34");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("EIGHTY-THREE MILLION, FOUR HUNDRED AND NINETY-ONE THOUSAND, TWO HUNDRED AND SEVENTY-FIVE DOLLARS AND THIRTY-FOUR CENTS"));

            await Input.FillAsync("98234.2");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("NINETY-EIGHT THOUSAND, TWO HUNDRED AND THIRTY-FOUR DOLLARS AND TWENTY CENTS"));

            await Input.FillAsync("543.00");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("FIVE HUNDRED AND FORTY-THREE DOLLARS"));

            await Input.FillAsync("897235.99");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("EIGHT HUNDRED AND NINETY-SEVEN THOUSAND, TWO HUNDRED AND THIRTY-FIVE DOLLARS AND NINETY-NINE CENTS"));

            await Input.FillAsync("8,9,3,2,4,5.");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("EIGHT HUNDRED AND NINETY-THREE THOUSAND, TWO HUNDRED AND FORTY-FIVE DOLLARS"));
        }

        [TestMethod]
        public async Task TestOneToTwenty()
        {
            await Input.FillAsync("1234567891011121314151617181.92");
            await SubmitButton.ClickAsync();
            // Expected string ends with '...NINETY-TWO CENTS' but actual string ends with '...NINETY CENTS'
            await Expect(Result).ToHaveTextAsync(new Regex("ONE OCTILLION, TWO HUNDRED AND THIRTY-FOUR SEPTILLION, FIVE HUNDRED AND SIXTY-SEVEN SEXTILLION, EIGHT HUNDRED AND NINETY-ONE QUINTILLION, ELEVEN QUADRILLION, ONE HUNDRED AND TWENTY-ONE TRILLION, THREE HUNDRED AND FOURTEEN BILLION, ONE HUNDRED AND FIFTY-ONE MILLION, SIX HUNDRED AND SEVENTEEN THOUSAND, ONE HUNDRED AND EIGHTY-ONE DOLLARS AND NINETY-TWO CENTS"));
            // The number is large enough for to be parsed as a decimal but C# rounds it to one decimal place because there are too many digits and so this test fails
        }


        [TestMethod]
        public async Task TestMaximum()
        {
            await Input.FillAsync("79,228,162,514,264,337,593,543,950,335");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("SEVENTY-NINE OCTILLION, TWO HUNDRED AND TWENTY-EIGHT SEPTILLION, ONE HUNDRED AND SIXTY-TWO SEXTILLION, FIVE HUNDRED AND FOURTEEN QUINTILLION, TWO HUNDRED AND SIXTY-FOUR QUADRILLION, THREE HUNDRED AND THIRTY-SEVEN TRILLION, FIVE HUNDRED AND NINETY-THREE BILLION, FIVE HUNDRED AND FORTY-THREE MILLION, NINE HUNDRED AND FIFTY THOUSAND, THREE HUNDRED AND THIRTY-FIVE DOLLARS"));
        }

        [TestMethod]
        public async Task TestInvalidInput()
        {
            await Input.FillAsync("abc");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("Please enter a valid number")); // Doesn't parse as a valid decimal

            await Input.FillAsync("783a");
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("Please enter a valid number")); // Doesn't parse as a valid decimal

            await Input.FillAsync("79,228,162,514,264,337,593,543,950,336"); // Larger than maximum decimal number in C#
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex("Please enter a valid number")); // Doesn't parse as a valid decimal

            await Input.FillAsync("-1"); // Out of range
            await SubmitButton.ClickAsync();
            await Expect(Result).ToHaveTextAsync(new Regex($"Out of range"));
        }
    }
}