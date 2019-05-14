import {Subject} from 'rxjs/internal/Subject';

import {PubSubMessage} from './pub-sub-message.model';

export class PubSubEventEmitter extends Subject<PubSubMessage> {
    constructor() {
        super();
    }

    emit(value) {
        super.next(value);
    }
}
