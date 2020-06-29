import { Request, Response } from 'express';
import mock from 'mockjs'
import Activity, { Atteandee } from '@/@types/activity/activity';

export default {
    'GET /api/activities/1': (req: Request, res: Response) => {
        const activity: Activity = {
            title: mock.Random.csentence(10, 30),
            content: `## ${mock.Random.csentence(10,20)} \n ![image](${mock.Random.image("1920x1080")}) \n\n ${mock.Random.cparagraph(5, 50)}`,
            endRegisterTime: new Date(),
            activityStartTime: new Date(),
            activityEndTime: new Date(),
            isCollected: false,
            isCreator: false,
            isJoined: false,
            activityStatusId: 1,
            showAddress: false
        };

        res.send(activity)
    },
    'GET /api/activities/1/attendees': (req: Request, res: Response) => {
        const attendees: Atteandee[] = [{
            userId: mock.Random.guid(),
            nickname: mock.Random.cname(),
            avatar: mock.Random.image(),
            gender: mock.Random.integer(0, 1),
            joinTime: new Date(),
            isOwner: true
        }];

        res.send(attendees)
    }
}