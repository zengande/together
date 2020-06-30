import React, { PureComponent } from "react";
import { PrismLight as SyntaxHighlighter } from "react-syntax-highlighter";
// 设置高亮样式
import { idea  } from "react-syntax-highlighter/dist/esm/styles/hljs";

interface CodeBlockProps {
    value: string;
    language: string;
}

class CodeBlock extends PureComponent<CodeBlockProps> {
    static defaultProps = {
        language: null
    };

    render() {
        const { language, value } = this.props;
        return (
            <figure className="highlight">
                <SyntaxHighlighter language={language} style={idea}>
                    {value}
                </SyntaxHighlighter>
            </figure>
        );
    }
}
export default CodeBlock;