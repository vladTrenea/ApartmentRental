export class PageChangeEvent {
    section: string;
    subsection: string;

    constructor(section: string, subsection: string) {
        this.section = section;
        this.subsection = subsection;
    }
}
