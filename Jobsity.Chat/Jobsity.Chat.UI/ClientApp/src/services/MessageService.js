import Service from "./Service";

export default class MessageService extends Service
{
    saveAndSend(body) {
        return this.post({ url: '/chat/message/send', body });
    }

    getLastestMessages(receiverId, receiverType, pageSize = 50, page = 1) {
        return this.get({ 
            url: '/chat/message/lastest', 
            params: { receiverId, receiverType, page, pageSize } 
        });
    }
}