﻿using System;

namespace Foster.Framework
{
    /// <summary>
    /// A 2D Circle
    /// </summary>
    public struct Circle : IProjectable2D
    {

        public Vector2 Position;
        public float Radius;

        public Circle(Vector2 position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        public Circle(float x, float y, float radius)
        {
            Position = new Vector2(x, y);
            Radius = radius;
        }

        public bool Overlaps(in Circle other, out Vector2 pushout)
        {
            pushout = Vector2.Zero;

            var lengthSqrd = (other.Position - Position).LengthSquared;
            if (lengthSqrd < (Radius + other.Radius) * (Radius + other.Radius))
            {
                var length = MathF.Sqrt(lengthSqrd);
                pushout = ((Position - other.Position) / length) * (Radius + other.Radius - length);
                return true;
            }

            return false;
        }

        public bool Overlaps(in IConvexShape2D shape, out Vector2 pushout)
        {
            pushout = Vector2.Zero;

            if (shape.Overlaps(this, out var p))
            {
                pushout = -p;
                return true;
            }

            return false;
        }

        public void Project(in Vector2 axis, out float min, out float max)
        {
            min = Vector2.Dot(Position - axis * Radius, axis);
            max = Vector2.Dot(Position + axis * Radius, axis);
        }
    }
}
