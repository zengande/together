interface Activity {
    title: string;
    content: string;
    city?: string;
    county?: string;
    detailAddress?: string;
    longitude?: number;
    latitude?: number;
    endRegisterTime: Date;
    activityStartTime: Date;
    activityEndTime: Date;
    activityStatusId: ActivityStatus;
    showAddress: boolean;
    isCollected: boolean;
    isJoined: boolean;
    numOfP?: number;
    limitsNum?: number;
    isCreator: boolean;
}

export enum ActivityStatus {
    Recruitment = 1
}

export interface Atteandee {
    userId: string;
    nickname: string;
    avatar: string;
    gender: number;
    joinTime: Date;
    isOwner: boolean;
}

export interface ActivityInputModel {
    title: string;
    content: string;
    endRegisterTime: Date;
    activityStartTime: Date;
    activityEndTime: Date;
    limitsNum?: number;
    addressVisibleRuleId?: number;
    catalogId: number;
}

export default Activity;