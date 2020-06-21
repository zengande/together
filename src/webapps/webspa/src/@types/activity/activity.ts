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
    addressVisibleRuleId: number;
}

export enum ActivityStatus {
    Recruitment = 1
}

export interface Participant {
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