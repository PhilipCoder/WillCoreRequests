using CodeBuilder.JS.Builder.Comments;
using CodeBuilder.JS.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Parameters
{
    [TestClass]
    public class ParamterTypeCommentTests
    {
        [TestMethod]
        public void TestJSDateArray()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSDateArray(), "Dates").GetText();
            Assert.AreEqual("    * @param { Date[] } Dates", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSDateArrayPromise()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSDateArray() {IsPromise = true }, "Dates").GetText();
            Assert.AreEqual("    * @param { PromiseLike<Date[]> } Dates", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSClassArray()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSClassArray() { ClassName = "Person" }, "Classes").GetText();
            Assert.AreEqual("    * @param { Person[] } Classes", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSClassArrayPromise()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSClassArray() { IsPromise = true, ClassName = "Person" }, "Classes").GetText();
            Assert.AreEqual("    * @param { PromiseLike<Person[]> } Classes", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSNumberArray()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSNumberArray(), "Numbers").GetText();
            Assert.AreEqual("    * @param { Number[] } Numbers", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSNumberArrayPromise()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSNumberArray() { IsPromise = true }, "Numbers").GetText();
            Assert.AreEqual("    * @param { PromiseLike<Number[]> } Numbers", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSDate()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSDate(), "MyDate").GetText();
            Assert.AreEqual("    * @param { Date } MyDate", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSDatePromise()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSDate() { IsPromise = true }, "MyDate").GetText();
            Assert.AreEqual("    * @param { PromiseLike<Date> } MyDate", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSClass()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSClass() { ClassName = "Person" }, "Class").GetText();
            Assert.AreEqual("    * @param { Person } Class", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSClassPromise()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSClass() { IsPromise = true, ClassName = "Person" }, "Class").GetText();
            Assert.AreEqual("    * @param { PromiseLike<Person> } Class", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSNumber()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSNumber(), "MyNumber").GetText();
            Assert.AreEqual("    * @param { Number } MyNumber", parameterTypeComment);
        }

        [TestMethod]
        public void TestJSNumberPromise()
        {
            var parameterTypeComment = new ParameterTypeComment(new JSNumber() { IsPromise = true }, "MyNumber").GetText();
            Assert.AreEqual("    * @param { PromiseLike<Number> } MyNumber", parameterTypeComment);
        }
    }
}
