﻿/******************************************************************************
 *
 *    MIConvexHull, Copyright (C) 2013 David Sehnal, Matthew Campbell
 *
 *  This library is free software; you can redistribute it and/or modify it 
 *  under the terms of  the GNU Lesser General Public License as published by 
 *  the Free Software Foundation; either version 2.1 of the License, or 
 *  (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful, 
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of 
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser 
 *  General Public License for more details.
 *  
 *****************************************************************************/

namespace MIConvexHull
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A factory class for creating a Voronoi mesh.
    /// </summary>
    public static class VoronoiMesh
    {
        /// <summary>
        /// Create the voronoi mesh.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TCell"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static VoronoiMesh<TVertex, TCell, TEdge> Create<TVertex, TCell, TEdge>(IEnumerable<TVertex> data)
            where TCell : TriangulationCell<TVertex, TCell>, new()
            where TVertex : IVertexPosition
            where TEdge : VoronoiEdge<TVertex, TCell>, new()
        {
            return VoronoiMesh<TVertex, TCell, TEdge>.Create(data);
        }

        /// <summary>
        /// Create the voronoi mesh.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static VoronoiMesh<TVertex, DefaultTriangulationCell<TVertex>, VoronoiEdge<TVertex, DefaultTriangulationCell<TVertex>>> Create<TVertex>(IEnumerable<TVertex> data)
            where TVertex : IVertexPosition
        {
            return VoronoiMesh<TVertex, DefaultTriangulationCell<TVertex>, VoronoiEdge<TVertex, DefaultTriangulationCell<TVertex>>>.Create(data);
        }

        /// <summary>
        /// Create the voronoi mesh.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static VoronoiMesh<DefaultVertex, DefaultTriangulationCell<DefaultVertex>, VoronoiEdge<DefaultVertex, DefaultTriangulationCell<DefaultVertex>>> 
            Create(IEnumerable<double[]> data)
        {
            var points = data.Select(p => new DefaultVertex { Position = p.ToArray() });
            return VoronoiMesh<DefaultVertex, DefaultTriangulationCell<DefaultVertex>, VoronoiEdge<DefaultVertex, DefaultTriangulationCell<DefaultVertex>>>.Create(points);
        }

        /// <summary>
        /// Create the voronoi mesh.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TCell"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static VoronoiMesh<TVertex, TCell, VoronoiEdge<TVertex, TCell>> Create<TVertex, TCell>(IEnumerable<TVertex> data)
            where TVertex : IVertexPosition
            where TCell : TriangulationCell<TVertex, TCell>, new()
        {
            return VoronoiMesh<TVertex, TCell, VoronoiEdge<TVertex, TCell>>.Create(data);
        }
    }

    /// <summary>
    /// A representation of a voronoi mesh.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TCell"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public class VoronoiMesh<TVertex, TCell, TEdge>
        where TCell : TriangulationCell<TVertex, TCell>, new()
        where TVertex : IVertexPosition
        where TEdge : VoronoiEdge<TVertex, TCell>, new()
    {
        /// <summary>
        /// This is probably not needed, but might make things a tiny bit faster.
        /// </summary>
        class EdgeComparer : IEqualityComparer<TEdge>
        {
            public bool Equals(TEdge x, TEdge y)
            {
                return (x.Source == y.Source && x.Target == y.Target) || (x.Source == y.Target && x.Target == y.Source);
            }

            public int GetHashCode(TEdge obj)
            {
                return obj.Source.GetHashCode() ^ obj.Target.GetHashCode();
            }
        }

        /// <summary>
        /// Vertices of the diagram.
        /// </summary>
        public IEnumerable<TCell> Cells { get; private set; }

        /// <summary>
        /// Edges connecting the cells. 
        /// The same information can be retrieved Cells' Adjacency.
        /// </summary>
        public IEnumerable<TEdge> Edges { get; private set; }

        /// <summary>
        /// Create a Voronoi diagram of the input data.
        /// </summary>
        /// <param name="data"></param>
        public static VoronoiMesh<TVertex, TCell, TEdge> Create(IEnumerable<TVertex> data)
        {
            if (data == null) 
                throw new ArgumentNullException("data can't be null");

            if (!(data is IList<TVertex>)) 
                data = data.ToArray();

            var t = DelaunayTriangulation<TVertex, TCell>.Create(data);
            var cells = t.Cells;
            var edges = new HashSet<TEdge>(new EdgeComparer());

            foreach (var c in cells)
            {
                for (int i = 0; i < c.Adjacency.Length; i++)
                {
                    var af = c.Adjacency[i];
                    if (af != null) 
                        edges.Add(new TEdge { Source = c, Target = af });
                }
            }

            return new VoronoiMesh<TVertex, TCell, TEdge>
            {
                Cells = cells,
                Edges = edges.ToList()
            };
        }

        /// <summary>
        /// Can only be created using a factory method.
        /// </summary>
        private VoronoiMesh()
        {

        }
    }
}
