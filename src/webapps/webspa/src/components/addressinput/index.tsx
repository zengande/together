import React from 'react';
import { Row, Col, Select } from 'antd'
import { useState } from 'react';

interface AddressValue {
    city?: string;
    county?: string;
}

interface AddressInputProps {
    value?: AddressValue;
    onChange?: (value: AddressValue) => void;
}

const Cities = [{ name: '杭州', id: 1 }, { name: '上海', id: 2 }]
const Counties = [
    { "name": "临安", "id": "129379" },
    { "name": "富阳", "id": "129378" },
    { "name": "建德", "id": "129377" },
    { "name": "上城区", "id": "129367" },
    { "name": "下城区", "id": "129368" },
    { "name": "江干区", "id": "129369" },
    { "name": "拱墅区", "id": "129370" },
    { "name": "西湖区", "id": "129371" },
    { "name": "滨江区", "id": "129372" },
    { "name": "萧山区", "id": "129373" },
    { "name": "余杭区", "id": "129374" },
    { "name": "桐庐县", "id": "129375" },
    { "name": "淳安县", "id": "129376" }
]

const AddressInput: React.FC<AddressInputProps> = ({ value, onChange }) => {
    const cities = Cities.map(c => ({ label: c.name, value: c.name }));
    const [counties, setCounties] = useState(Counties.map(c => ({ label: c.name, value: c.name })));

    let { city: _city } = value || {};
    if (!_city) {
        _city = cities[0].value
    }

    const [city, setCity] = useState(_city)
    const [county, setCounty] = useState(counties[0].value)

    const triggerChange = (changedValue: any) => {
        if (onChange) {
            onChange({ city, ...value, ...changedValue });
        }
    };

    return (
        <div>
            <Row gutter={12}>
                <Col span={8}>
                    <Select options={cities} value={city} />
                </Col>
                <Col span={8}>
                    <Select options={counties} value={county} />
                </Col>
            </Row>
        </div>
    )
}

export default AddressInput;