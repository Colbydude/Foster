﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Foster.Framework
{
    public struct Cardinal
    {
        public const byte VAL_RIGHT = 0;
        public const byte VAL_UP = 1;
        public const byte VAL_LEFT = 2;
        public const byte VAL_DOWN = 3;

        static public readonly Cardinal Right = new Cardinal(VAL_RIGHT);
        static public readonly Cardinal Up = new Cardinal(VAL_UP);
        static public readonly Cardinal Left = new Cardinal(VAL_LEFT);
        static public readonly Cardinal Down = new Cardinal(VAL_DOWN);

        public byte Value { get; private set; }

        private Cardinal(byte val)
        {
            Value = val;
        }

        public Cardinal Reverse => new Cardinal((byte)((Value + 2) % 4));
        public Cardinal TurnRight => new Cardinal((byte)((Value + 3) % 4));
        public Cardinal TurnLeft => new Cardinal((byte)((Value + 1) % 4));

        public bool Horizontal => Value % 2 == 0;
        public bool Vertical => Value % 2 == 1;    

        public int X
        {
            get
            {
                switch (Value)
                {
                    case 0:
                        return 1;
                    case 2:
                        return -1;
                    case 1:
                    case 3:
                        return 0;
                }

                throw new ArgumentException();
            }
        }

        public int Y
        {
            get
            {
                switch (Value)
                {
                    case 3:
                        return 1;
                    case 1:
                        return -1;
                    case 0:
                    case 2:
                        return 0;
                }

                throw new ArgumentException();
            }
        }

        public float Angle
        {
            get
            {
                switch (Value)
                {
                    case 0:
                        return 0;
                    case 1:
                        return -Calc.HalfPI;
                    case 2:
                        return Calc.PI;
                    case 3:
                        return Calc.HalfPI;
                }

                throw new ArgumentException();
            }
        }

        static public implicit operator Cardinal(Facing f) => f.ToByte() == 0 ? Cardinal.Right : Cardinal.Left;
        static public implicit operator Point2(Cardinal c) => new Point2(c.X, c.Y);
        static public bool operator ==(Cardinal a, Cardinal b) => a.Value == b.Value;
        static public bool operator !=(Cardinal a, Cardinal b) => a.Value != b.Value;
        static public Point2 operator *(Cardinal a, int b) => (Point2)a * b;

        public override int GetHashCode()
        {
            return Value;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Cardinal c))
                return false;
            else
                return this == c;
        }

        public override string ToString()
        {
            switch (Value)
            {
                case 0: return "Right";
                case 1: return "Up";
                case 2: return "Left";
                default: return "Down";
            }
        }

        public byte ToByte()
        {
            return Value;
        }

        public static Cardinal FromByte(byte v)
        {
            Debug.Assert(v < 4, "Argument out of range");
            return new Cardinal(v);
        }

        public static Cardinal FromVector(Vector2 dir)
        {
            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
                return dir.X < 0 ? Left : Right;
            return dir.Y < 0 ? Up : Down;
        }

        public static Cardinal FromVector(float x, float y)
        {
            if (Math.Abs(x) > Math.Abs(y))
                return x < 0 ? Left : Right;
            return y < 0 ? Up : Down;
        }

        public static Cardinal FromPoint(Point2 dir)
        {
            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
                return dir.X < 0 ? Left : Right;
            return dir.Y < 0 ? Up : Down;
        }

        public static Cardinal FromPoint(int x, int y)
        {
            if (Math.Abs(x) > Math.Abs(y))
                return x < 0 ? Left : Right;
            return y < 0 ? Up : Down;
        }

        public static IEnumerable<Cardinal> All
        {
            get
            {
                yield return Right;
                yield return Down;
                yield return Left;
                yield return Up;
            }
        }
    }
}
