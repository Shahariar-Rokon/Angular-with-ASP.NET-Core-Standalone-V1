import { Skills } from "./skills";
export class CandidateSkills {
    constructor(
        public candidateId?: number,
        public candidateName?: string,
        public birthDate?: Date,
        public phoneNo?: number,
        public picture?: string,
        public pictureFile?: File,
        public fresher?: boolean,
        public skillList?: Skills[]
      ) { }
}
