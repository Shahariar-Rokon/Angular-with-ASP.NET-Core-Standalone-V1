import { Skills } from "./skills";
export class Candidates {
    constructor(
        public candidateId?: number,
        public candidateName?: string,
        public dateOfBirth?: Date,
        public mobileNo?: string,
        public picture?: string,
        public pictureFile?: File,
        public isFresher?: boolean,
        public skillList?: Skills[]
      ) { }
}
