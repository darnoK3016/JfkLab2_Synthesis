namespace Synthesis
{
    using System;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal sealed class Rewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (!@"Log".Equals(node.Identifier.ValueText, StringComparison.Ordinal))
            {
                var firstNode = node.ChildNodes().OfType<BlockSyntax>().FirstOrDefault();
                var arguments = firstNode?.ChildNodes().OfType<ReturnStatementSyntax>().FirstOrDefault()?.ChildNodes().FirstOrDefault().ToString();
                if (null != arguments)
                {
                    var leadingTrivia = firstNode?.ChildNodes().FirstOrDefault().GetLeadingTrivia();
 
                    var loggerExpression = SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.InvocationExpression(
                            SyntaxFactory.MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("Logger").WithLeadingTrivia(leadingTrivia), SyntaxFactory.IdentifierName("Log")),
                                                        SyntaxFactory.ParseArgumentList($"({"nameof(" + node.Identifier.ValueText}), {arguments})")
                                        ));
                    node = node.InsertNodesBefore(firstNode?.ChildNodes().FirstOrDefault(), Enumerable.Repeat(loggerExpression.WithTrailingTrivia(SyntaxFactory.EndOfLine(Environment.NewLine)), 1));
                }
            }

            return base.VisitMethodDeclaration(node);
        }
    }
}
