
// this file is generated — do not edit it


/// <reference types="@sveltejs/kit" />

/**
 * Environment variables [loaded by Vite](https://vitejs.dev/guide/env-and-mode.html#env-files) from `.env` files and `process.env`. Like [`$env/dynamic/private`](https://kit.svelte.dev/docs/modules#$env-dynamic-private), this module cannot be imported into client-side code. This module only includes variables that _do not_ begin with [`config.kit.env.publicPrefix`](https://kit.svelte.dev/docs/configuration#env).
 * 
 * _Unlike_ [`$env/dynamic/private`](https://kit.svelte.dev/docs/modules#$env-dynamic-private), the values exported from this module are statically injected into your bundle at build time, enabling optimisations like dead code elimination.
 * 
 * ```ts
 * import { API_KEY } from '$env/static/private';
 * ```
 * 
 * Note that all environment variables referenced in your code should be declared (for example in an `.env` file), even if they don't have a value until the app is deployed:
 * 
 * ```
 * MY_FEATURE_FLAG=""
 * ```
 * 
 * You can override `.env` values from the command line like so:
 * 
 * ```bash
 * MY_FEATURE_FLAG="enabled" npm run dev
 * ```
 */
declare module '$env/static/private' {
	export const LESS_TERMCAP_se: string;
	export const npm_package_dev: string;
	export const MAIL: string;
	export const USER: string;
	export const LESS_TERMCAP_ue: string;
	export const npm_config_user_agent: string;
	export const SSH_AGENT_PID: string;
	export const npm_node_execpath: string;
	export const npm_package_resolved: string;
	export const SHLVL: string;
	export const HISTIGNORE: string;
	export const npm_config_noproxy: string;
	export const HOME: string;
	export const OLDPWD: string;
	export const npm_package_optional: string;
	export const XDG_SESSION_COOKIE: string;
	export const npm_package_json: string;
	export const npm_package_engines_node: string;
	export const LESS_TERMCAP_so: string;
	export const npm_config_userconfig: string;
	export const npm_config_local_prefix: string;
	export const npm_package_integrity: string;
	export const DBUS_SESSION_BUS_ADDRESS: string;
	export const npm_config_engine_strict: string;
	export const COLORTERM: string;
	export const COLOR: string;
	export const npm_config_metrics_registry: string;
	export const VDPAU_DRIVER: string;
	export const LOGNAME: string;
	export const ALACRITTY_SOCKET: string;
	export const WINDOWID: string;
	export const LESS_TERMCAP_us: string;
	export const _: string;
	export const npm_config_prefix: string;
	export const XDG_SESSION_CLASS: string;
	export const TERM: string;
	export const ALACRITTY_LOG: string;
	export const npm_config_cache: string;
	export const npm_config_node_gyp: string;
	export const PATH: string;
	export const NODE: string;
	export const npm_package_name: string;
	export const XDG_RUNTIME_DIR: string;
	export const DISPLAY: string;
	export const HISTSIZE: string;
	export const LANG: string;
	export const XAUTHORITY: string;
	export const npm_lifecycle_script: string;
	export const SSH_AUTH_SOCK: string;
	export const SHELL: string;
	export const npm_package_version: string;
	export const npm_lifecycle_event: string;
	export const LESS_TERMCAP_mb: string;
	export const npm_package_dev_optional: string;
	export const LESS_TERMCAP_md: string;
	export const ALACRITTY_WINDOW_ID: string;
	export const npm_config_globalconfig: string;
	export const npm_config_init_module: string;
	export const npm_package_peer: string;
	export const JAVA_HOME: string;
	export const GPG_TTY: string;
	export const PWD: string;
	export const LESS_TERMCAP_me: string;
	export const npm_execpath: string;
	export const ANDROID_HOME: string;
	export const npm_config_global_prefix: string;
	export const npm_command: string;
	export const INIT_CWD: string;
	export const EDITOR: string;
}

/**
 * Similar to [`$env/static/private`](https://kit.svelte.dev/docs/modules#$env-static-private), except that it only includes environment variables that begin with [`config.kit.env.publicPrefix`](https://kit.svelte.dev/docs/configuration#env) (which defaults to `PUBLIC_`), and can therefore safely be exposed to client-side code.
 * 
 * Values are replaced statically at build time.
 * 
 * ```ts
 * import { PUBLIC_BASE_URL } from '$env/static/public';
 * ```
 */
declare module '$env/static/public' {

}

/**
 * This module provides access to runtime environment variables, as defined by the platform you're running on. For example if you're using [`adapter-node`](https://github.com/sveltejs/kit/tree/master/packages/adapter-node) (or running [`vite preview`](https://kit.svelte.dev/docs/cli)), this is equivalent to `process.env`. This module only includes variables that _do not_ begin with [`config.kit.env.publicPrefix`](https://kit.svelte.dev/docs/configuration#env).
 * 
 * This module cannot be imported into client-side code.
 * 
 * ```ts
 * import { env } from '$env/dynamic/private';
 * console.log(env.DEPLOYMENT_SPECIFIC_VARIABLE);
 * ```
 * 
 * > In `dev`, `$env/dynamic` always includes environment variables from `.env`. In `prod`, this behavior will depend on your adapter.
 */
declare module '$env/dynamic/private' {
	export const env: {
		LESS_TERMCAP_se: string;
		npm_package_dev: string;
		MAIL: string;
		USER: string;
		LESS_TERMCAP_ue: string;
		npm_config_user_agent: string;
		SSH_AGENT_PID: string;
		npm_node_execpath: string;
		npm_package_resolved: string;
		SHLVL: string;
		HISTIGNORE: string;
		npm_config_noproxy: string;
		HOME: string;
		OLDPWD: string;
		npm_package_optional: string;
		XDG_SESSION_COOKIE: string;
		npm_package_json: string;
		npm_package_engines_node: string;
		LESS_TERMCAP_so: string;
		npm_config_userconfig: string;
		npm_config_local_prefix: string;
		npm_package_integrity: string;
		DBUS_SESSION_BUS_ADDRESS: string;
		npm_config_engine_strict: string;
		COLORTERM: string;
		COLOR: string;
		npm_config_metrics_registry: string;
		VDPAU_DRIVER: string;
		LOGNAME: string;
		ALACRITTY_SOCKET: string;
		WINDOWID: string;
		LESS_TERMCAP_us: string;
		_: string;
		npm_config_prefix: string;
		XDG_SESSION_CLASS: string;
		TERM: string;
		ALACRITTY_LOG: string;
		npm_config_cache: string;
		npm_config_node_gyp: string;
		PATH: string;
		NODE: string;
		npm_package_name: string;
		XDG_RUNTIME_DIR: string;
		DISPLAY: string;
		HISTSIZE: string;
		LANG: string;
		XAUTHORITY: string;
		npm_lifecycle_script: string;
		SSH_AUTH_SOCK: string;
		SHELL: string;
		npm_package_version: string;
		npm_lifecycle_event: string;
		LESS_TERMCAP_mb: string;
		npm_package_dev_optional: string;
		LESS_TERMCAP_md: string;
		ALACRITTY_WINDOW_ID: string;
		npm_config_globalconfig: string;
		npm_config_init_module: string;
		npm_package_peer: string;
		JAVA_HOME: string;
		GPG_TTY: string;
		PWD: string;
		LESS_TERMCAP_me: string;
		npm_execpath: string;
		ANDROID_HOME: string;
		npm_config_global_prefix: string;
		npm_command: string;
		INIT_CWD: string;
		EDITOR: string;
		[key: `PUBLIC_${string}`]: undefined;
		[key: string]: string | undefined;
	}
}

/**
 * Similar to [`$env/dynamic/private`](https://kit.svelte.dev/docs/modules#$env-dynamic-private), but only includes variables that begin with [`config.kit.env.publicPrefix`](https://kit.svelte.dev/docs/configuration#env) (which defaults to `PUBLIC_`), and can therefore safely be exposed to client-side code.
 * 
 * Note that public dynamic environment variables must all be sent from the server to the client, causing larger network requests — when possible, use `$env/static/public` instead.
 * 
 * ```ts
 * import { env } from '$env/dynamic/public';
 * console.log(env.PUBLIC_DEPLOYMENT_SPECIFIC_VARIABLE);
 * ```
 */
declare module '$env/dynamic/public' {
	export const env: {
		[key: `PUBLIC_${string}`]: string | undefined;
	}
}
