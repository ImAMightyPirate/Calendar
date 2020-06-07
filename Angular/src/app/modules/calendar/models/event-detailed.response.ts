import { EventSummaryResponse } from './event-summary.response';

export interface EventDetailedResponse extends EventSummaryResponse {
  isDeleted: boolean;
}
