﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;

namespace osu.Framework.Graphics.OpenGL.Vertices
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ParticleVertex2D : IEquatable<ParticleVertex2D>, IVertex
    {
        [VertexMember(2, VertexAttribPointerType.Float)]
        public Vector2 Position;
        [VertexMember(4, VertexAttribPointerType.Float)]
        public Color4 Colour;
        [VertexMember(2, VertexAttribPointerType.Float)]
        public Vector2 TexturePosition;
        [VertexMember(1, VertexAttribPointerType.Float)]
        public float Time;
        [VertexMember(2, VertexAttribPointerType.Float)]
        public Vector2 Direction;

        public bool Equals(ParticleVertex2D other)
        {
            return Position.Equals(other.Position) && TexturePosition.Equals(other.TexturePosition) && Colour.Equals(other.Colour) && Time.Equals(other.Time) && Direction.Equals(other.Direction);
        }
    }
}
