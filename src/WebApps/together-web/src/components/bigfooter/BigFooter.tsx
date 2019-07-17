import React from 'react';
import { Row, Col } from 'antd';

export class BigFooter extends React.PureComponent {

    render() {
        return (
            <Row>
                <Col span={6}>1</Col>
                <Col span={6}>2</Col>
                <Col span={6}>3</Col>
                <Col span={6}>4</Col>
            </Row>
        )
    }
}