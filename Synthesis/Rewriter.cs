namespace Synthesis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal sealed class Rewriter : CSharpSyntaxRewriter
    {
        public List<ReturnStatementSyntax> blockList = new List<ReturnStatementSyntax>();
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (!@"Log".Equals(node.Identifier.ValueText, StringComparison.Ordinal))
            {
                search(node);
                foreach (var nodes in blockList)
                {
                    var arguments = nodes?.ChildNodes().FirstOrDefault().ToString();
                    if (null != arguments)
                    {
                        var leadingTrivia = nodes?.GetLeadingTrivia();
                        var loggerExpression = SyntaxFactory.ExpressionStatement(
                            SyntaxFactory.InvocationExpression(
                                SyntaxFactory.MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("Logger").WithLeadingTrivia(leadingTrivia), SyntaxFactory.IdentifierName("Log")),
                                                            SyntaxFactory.ParseArgumentList($"({"nameof(" + node.Identifier.ValueText}), {arguments})")
                                            ));

                        node = node.InsertNodesBefore(nodes, Enumerable.Repeat(loggerExpression.WithTrailingTrivia(SyntaxFactory.EndOfLine(Environment.NewLine)), 1));
                    }
                }
            }
            return base.VisitMethodDeclaration(node);
        }

        public void search(SyntaxNode node)
        {
            foreach (SyntaxNode childNode in node.ChildNodes())
            {
                search(childNode);
            }
            foreach (ReturnStatementSyntax childNode in node.ChildNodes().OfType<ReturnStatementSyntax>())
            {
                blockList.Add(childNode);
            }
        }
    }
}
