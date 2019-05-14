import {Injectable} from '@angular/core';

import {PubSubEventEmitter} from './pub-sub-event.emitter';
import {PubSubMessage} from './pub-sub-message.model';

@Injectable()
export class PubSubService {
    private stream: PubSubEventEmitter;

    constructor() {
        this.stream = new PubSubEventEmitter();
    }

    pub(type: string, value: any) {
        const pubSubMessage = new PubSubMessage(type, value);

        this.stream.emit(pubSubMessage);
    }

    sub(): PubSubEventEmitter {
        return this.stream;
    }
}
