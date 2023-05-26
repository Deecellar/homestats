<script lang="ts">
	import { goto } from '$app/navigation';
	import { env } from '$env/dynamic/public';
	import { AuthenticationRequest, Client, IConfig } from '$lib/clients';
	import { onMount } from 'svelte';
    import type { PageData } from '../../login/$types';


    let client = new Client(new IConfig(localStorage),env.PUBLIC_API_URL);
    console.log(env.PUBLIC_API_URL);
    async function login() {
      let email = document.getElementById('email-address') as HTMLInputElement;
      let password = document.getElementById('password') as HTMLInputElement;
      let rememberMe = document.getElementById('remember-me') as HTMLInputElement;
      // we get the value of the input fields
      let emailValue = email.value;
      let passwordValue = password.value;
      let rememberMeValue = rememberMe.checked;
      // we send the data to the server
      let response = await client.authenticate(new AuthenticationRequest({email: emailValue, password: passwordValue}));
      // We extract the token from the response
      let token = response.data?.jwToken;
      // We store the token in the local storage
      
      localStorage.setItem('token', token!);
      // We redirect the user to the dashboard
      goto("/stats");
    }

    onMount(async() => {
      let token = localStorage.removeItem('token');
    })
    
    export let data: PageData;
</script>

    <div class="flex min-h-full h-screen items-center justify-center py-12 px-4 sm:px-6 lg:px-8 bg-gray-900">
        
        <div class="w-full max-w-md space-y-8 pb-10">
          <div>
            <a href="/">
                <!-- Heroicon name: solid/arrow-small-left -->
                <svg class="w-6 h-6 text-indigo-600 hover:text-indigo-500" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18" />
                </svg>


        </a>

            <a href="/"><img class="mx-auto h-12 w-auto" src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600" alt="Your Company"></a>
            <h2 class="mt-6 text-center text-3xl font-bold tracking-tight text-gray-50">Ingresar a tu cuenta</h2>
            <p class="mt-2 text-center text-sm text-gray-300">
              o también puedes
              <a href="/register" class="font-medium text-indigo-500 hover:text-indigo-400">Crear una cuenta</a>
            </p>
          </div>
          <form class="mt-8 space-y-6" >
            <input type="hidden" name="remember" value="true">
            <div class="-space-y-px rounded-md shadow-sm">
              <div>
                <label for="email-address" class="sr-only">Email address</label>
                <input id="email-address" name="email" type="email" autocomplete="email" required class="relative block w-full rounded-t-md border-0 py-1.5 text-black ring-1 ring-inset ring-gray-700 placeholder:text-gray-800 focus:z-10 focus:ring-2 focus:ring-inset focus:ring-indigo-500 sm:text-sm sm:leading-6" placeholder="Correo Electrónico">
              </div>
              <div>
                <label for="password" class="sr-only">Password</label>
                <input id="password" name="password" type="password" autocomplete="current-password" required class="relative block w-full rounded-b-md border-0 py-1.5 text-black ring-1 ring-inset ring-gray-700 placeholder:text-gray-800 focus:z-10 focus:ring-2 focus:ring-inset focus:ring-indigo-500 sm:text-sm sm:leading-6" placeholder="Contraseña">
              </div>
            </div>
      
            <div class="flex items-center justify-between">
              <div class="flex items-center">
                <input id="remember-me" name="remember-me" type="checkbox" class="h-4 w-4 rounded border-gray-700 text-indigo-500 focus:ring-indigo-500">
                <label for="remember-me" class="ml-2 block text-sm text-gray-50">Recuerdame</label>
              </div>
      
              <div class="text-sm">
                <a href="/register" class="font-medium text-indigo-500 hover:text-indigo-400">¿No tienes cuenta? Registrate</a>
              </div>
            </div>
      
            <div>
              <button on:click={login} class="group relative flex w-full justify-center rounded-md bg-indigo-500 py-2 px-3 text-sm font-semibold text-white hover:bg-indigo-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500">
                <span class="absolute inset-y-0 left-0 flex items-center pl-3">
                  <svg class="h-5 w-5 text-indigo-400 group-hover:text-indigo-300" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                    <path fill-rule="evenodd" d="M10 1a4.5 4.5 0 00-4.5 4.5V9H5a2 2 0 00-2 2v6a2 2 0 002 2h10a2 2 0 002-2v-6a2 2 0 00-2-2h-.5V5.5A4.5 4.5 0 0010 1zm3 8V5.5a3 3 0 10-6 0V9h6z" clip-rule="evenodd" />
                  </svg>
                </span>
                Ingresar
              </button>
            </div>
          </form>
        </div>
      </div>
  
