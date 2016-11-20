using System;
using System.IO;
using FluentAssertions;
using FluentAssertions.Primitives;
using Nancy.Testing;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing;
using VDS.RDF.Writing.Formatting;
using StringWriter = System.IO.StringWriter;

namespace Lernaean.Hydra.Tests
{
    public static class TestExtension
    {
        public static IGraph AsRdf(this BrowserResponseBodyWrapper body)
        {
            IGraph graph = new Graph();
            using (var reader = new StreamReader(body.AsStream()))
            {
                new VDS.RDF.Parsing.TurtleParser().Load(graph, reader);
            }

            return graph;
        }

        public static GraphAssertions Should(this IGraph graph)
        {
            return new GraphAssertions(graph);
        }

        public class GraphAssertions : ReferenceTypeAssertions<IGraph, GraphAssertions>
        {
            private readonly IGraph graph;

            public GraphAssertions(IGraph graph)
            {
                this.graph = graph;
            }

            protected override string Context
            {
                get { return "Graph"; }
            }

            public void MatchAsk(SparqlQuery query)
            {
                var queryProcessor = new LeviathanQueryProcessor(new InMemoryDataset(this.graph));
                var processQuery = queryProcessor.ProcessQuery(query) as SparqlResultSet;

                processQuery.Result.Should().BeTrue("Actual triples were:{0}{0}{1}", Environment.NewLine, this.SerializeGraph());
            }

            private object SerializeGraph()
            {
                var writer = new CompressingTurtleWriter();
                TextWriter textWriter = new StringWriter();
                writer.Save(this.graph, textWriter);
                return textWriter.ToString();
            }
        }
    }
}