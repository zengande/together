import React from 'react';
import ReactMarkdown from 'react-markdown';
import CodeBlock from './codeblock';
import ImageViewer from './imageviewer';

interface MarkdownRenderProps {
    content?: string;
}

class MarkdownRender extends React.PureComponent<MarkdownRenderProps> {
    render() {
        const { content } = this.props;
        return (
            <ReactMarkdown source={content}
                renderers={{
                    "code": CodeBlock,
                    "image": ImageViewer,
                }} />
        )
    }
}

export default MarkdownRender;