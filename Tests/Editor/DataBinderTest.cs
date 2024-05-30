using System.Collections.Generic;
using System.Linq;
using DynamicExpresso;
using NUnit.Framework;
using UnityEngine;

namespace BaseTool.Tests.Editor
{
    public class DataBinderTest
    {
        public enum MyEnum { A, B };

        [Test]
        public void DataBinderToolTest()
        {
            var interpreter = new Interpreter();
            Assert.IsTrue(interpreter.Eval<bool>("true"));
        }

        [Test]
        public void DataBinderComparerTest()
        {
            var interpreter = new Interpreter();
            var comp = interpreter.Eval<bool>("1 < 2");
            Assert.IsTrue(comp);
        }

        [Test]
        public void DataBinderComparerTestWithVector2Int()
        {
            var interpreter = new Interpreter();
            var vec = new Vector2Int(0, 1);
            interpreter.SetVariable("this", vec);
            var comp = interpreter.Eval<bool>("y > 0");
            Assert.IsTrue(comp);
        }

        [Test]
        public void DataBinderComparerTestWithVector2IntCastObject()
        {
            var interpreter = new Interpreter();
            object vec = new Vector2Int(0, 1);
            interpreter.SetVariable("this", vec);
            var comp = interpreter.Eval<bool>("y > 0");
            Assert.IsTrue(comp);
        }

        [Test]
        public void DataBinderComparerTestWithGameObjectCastObject()
        {
            var interpreter = new Interpreter();
            Object vec = new GameObject("test");
            interpreter.SetVariable("this", vec);
            var comp = interpreter.Eval<bool>("name == \"Test\"");
            Assert.IsFalse(comp);
        }

        [Test]
        public void DataBinderComparerTestWithEnum()
        {
            var interpreter = new Interpreter();
            var a = MyEnum.A;
            interpreter.SetVariable("a", a);
            interpreter.Reference(GetEveryEnums());
            var comp = interpreter.Eval<bool>("a == MyEnum.A");
            Assert.True(comp);
        }

        [Test]
        public void DataBinderComparerTestWithEnumIncluded()
        {
            var interpreter = new Interpreter();
            var a = MyEnum.A;
            interpreter.SetVariable("a", a);
            interpreter.Reference(typeof(MyEnum));
            var comp = interpreter.Eval<bool>("a == MyEnum.A");
            Assert.True(comp);
        }

        protected List<ReferenceType> GetEveryEnums()
        {
            var assembly = GetType().Assembly;
            var everyTypes = assembly.GetTypes();
            return everyTypes.Select(t => new ReferenceType(t.Name, t)).ToList();
        }
    }
}
