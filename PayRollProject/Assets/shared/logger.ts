const DEBUG = import.meta.env.DEV;

export function log (...args : unknown[]):void {
	if (DEBUG) console.log(...args);
}

export function warn (...args : unknown[]) : void {
	if (DEBUG) console.warn(...args);
}

export function error (...args : unknown[]) : void {
	if (DEBUG) console.error(...args);
}