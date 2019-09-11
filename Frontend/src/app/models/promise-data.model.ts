export enum PromiseStatus {
  Nothing = 0,
  Done = 1,
  NotPerformed = 2
}

export class PromiseData {
  public description: string;
  public status: PromiseStatus;
  public source: string;
}
