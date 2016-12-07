using System;
using System.IO;
using FluentAssertions;
using FluentAssertions.Primitives;
using Nancy.Testing;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing;
using StringWriter = System.IO.StringWriter;

namespace Argolis.Tests.Integration
{
    public static class TestExtension
    {
        public static IGraph AsRdf(this BrowserResponseBodyWrapper body)
        {
            IGraph graph = new Graph();
            graph.NamespaceMap.AddNamespace("hydra", new Uri(Vocab.Hydra.BaseUri));
            graph.NamespaceMap.AddNamespace("ex", new Uri("http://example.api/o#"));

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
                const string failFormat = "Actual triples were:{0}{0}{1}{0}The query was: {0}{0}{2}";

                var queryProcessor = new LeviathanQueryProcessor(new InMemoryDataset(this.graph));
                var processQuery = (SparqlResultSet)queryProcessor.ProcessQuery(query);

                processQuery.Result.Should().BeTrue(failFormat, Environment.NewLine, this.SerializeGraph(), query);
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