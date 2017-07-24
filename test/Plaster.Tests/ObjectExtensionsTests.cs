using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Plaster.Tests
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void should_throw_an_exception_when_is_not_a_class()
        {
            Assert.Throws<NotSupportedException>(() => default(int).GetDocument("123"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void should_throw_an_exception_when_id_is_null_or_empty(string id)
        {
            Assert.Throws<NotSupportedException>(() => default(int).GetDocument(id));
        }

        [Fact]
        public void should_return_the_correct_document()
        {
            var result = default(Doc).GetDocument("123");

            result.Should().NotBeNull();
        }

        [Fact]
        public void should_return_the_correct_document_with_not_null_properties_assigned()
        {
            var obj = new Doc {Test = "123"};

            var result = obj.GetDocument("123");

            result.Should().NotBeNull();

            foreach (var prop in typeof(Doc).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                result.Fields.Keys.Should().Contain(prop.Name);
                result.Fields.FirstOrDefault(_ => _.Key == prop.Name).Value.Value.Should().NotBeNull();
            }
        }

        [Fact]
        public void should_return_the_correct_document_without_null_properties_assigned()
        {
            var obj = new Doc();

            var result = obj.GetDocument("123");

            result.Should().NotBeNull();

            foreach (var prop in typeof(Doc).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (prop.GetValue(obj) != null) return;
                result.Fields.Keys.Should().NotContain(prop.Name);
            }
        }

        [Fact]
        public void should_add_id_to_the_document()
        {
            var obj = new Doc();

            var result = obj.GetDocument("123");

            result.Fields.Keys.Should().Contain("_id");
        }

        [Fact]
        public void should_add_set_the_current_Id_to_the_document()
        {
            var obj = new DocWithId {Id = "123"};

            var result = obj.GetDocument(null);

            result.Fields.Keys.Should().Contain("_id");

            var prop = obj.GetType().GetProperties().FirstOrDefault(p => p.Name.ToLower() == "id");

            result.Fields.FirstOrDefault(_ => _.Key == "_id").Value.Value.Should().Be((string) prop.GetValue(obj));
        }

        [Fact]
        public void should_add_set_the_current_id_to_the_document()
        {
            var obj = new DocWithid { id = "123" };

            var result = obj.GetDocument(null);

            result.Fields.Keys.Should().Contain("_id");

            var prop = obj.GetType().GetProperties().FirstOrDefault(p => p.Name.ToLower() == "id");

            result.Fields.FirstOrDefault(_ => _.Key == "_id").Value.Value.Should().Be((string)prop.GetValue(obj));
        }

        [Fact]
        public void should_add_set_the_current_ID_to_the_document()
        {
            var obj = new DocWithID { ID = "123" };

            var result = obj.GetDocument(null);

            result.Fields.Keys.Should().Contain("_id");

            var prop = obj.GetType().GetProperties().FirstOrDefault(p => p.Name.ToLower() == "id");

            result.Fields.FirstOrDefault(_ => _.Key == "_id").Value.Value.Should().Be((string)prop.GetValue(obj));
        }
    }

    public class Doc
    {
        public string Test { get; set; }
    }

    public class DocWithId
    {
        public string Id { get; set; }
    }

    public class DocWithid
    {
        public string id { get; set; }
    }

    public class DocWithID
    {
        public string ID { get; set; }
    }
}
