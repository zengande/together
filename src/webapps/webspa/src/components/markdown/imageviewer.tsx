import React from 'react';
import ReactZmage from 'react-zmage';

interface ImageViewerProps {
    src: string;
}
class ImageViewer extends React.PureComponent<ImageViewerProps> {
    render(): JSX.Element {
        const { src } = this.props;
        console.log(src)

        return (
            <ReactZmage src={src} alt="图片"/>
        )
    }
}
export default ImageViewer;