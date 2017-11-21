﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public sealed class Path
    {
        private readonly IList<IEdge> _edges = new List<IEdge>();

        public IVertex Start { get; private set; }

        public IVertex End { get; private set; }

        public double Cost
        {
            get
            {
                double c = 0;
                foreach (IEdge e in _edges)
                    c += e.Weight;
                return c;
            }
        }

        public IQueryable<IEdge> Edges
        {
            get
            {
                return _edges.AsQueryable();
            }
        }

        public Path(IVertex start)
        {
            Start = start;
            End = start;
        }

        public void AddEdge(IEdge e)
        {
            if (!e.IsEdgeOf(End))
                throw new ArgumentException("Wrong edge");
            _edges.Add(e);
            End = e.GetOppositeOf(End);
        }

        public override string ToString()
        {
            var resp = $"Path: [{Start}";

            if (Edges.Any())
            {
                IVertex vertex = Start;
                foreach (IEdge edge in _edges)
                {
                    vertex = edge.GetOppositeOf(vertex);
                    resp = $"{resp} -> {vertex} ({edge})";
                }
                resp = $"{resp}]";
            }
            else
            {
                resp = $"{resp} -> {End}]";
            }
            return resp;
        }
    }
}