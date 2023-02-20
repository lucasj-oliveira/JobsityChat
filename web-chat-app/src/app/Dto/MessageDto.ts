import * as moment from 'moment';


export class MessageDto {
    public user: string = '';
    public msgText: string = '';
    public time:  moment.Moment = moment();
}