import type { ParamMatcher } from '@sveltejs/kit';
 
export const match = ((param) => {
  // We match a guiid
  return param;
}) satisfies ParamMatcher;