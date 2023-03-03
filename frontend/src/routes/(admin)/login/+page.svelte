<script lang="ts">
    import type { PageData } from '../../login/$types';
    
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
            <h2 class="mt-6 text-center text-3xl font-bold tracking-tight text-gray-50">Sign in to your account</h2>
            <p class="mt-2 text-center text-sm text-gray-300">
              Or
              <a href="/register" class="font-medium text-indigo-500 hover:text-indigo-400">create an account</a>
            </p>
          </div>
          <form class="mt-8 space-y-6" action="#" method="POST">
            <input type="hidden" name="remember" value="true">
            <div class="-space-y-px rounded-md shadow-sm">
              <div>
                <label for="email-address" class="sr-only">Email address</label>
                <input id="email-address" name="email" type="email" autocomplete="email" required class="relative block w-full rounded-t-md border-0 py-1.5 text-gray-50 ring-1 ring-inset ring-gray-700 placeholder:text-gray-800 focus:z-10 focus:ring-2 focus:ring-inset focus:ring-indigo-500 sm:text-sm sm:leading-6" placeholder="Email address">
              </div>
              <div>
                <label for="password" class="sr-only">Password</label>
                <input id="password" name="password" type="password" autocomplete="current-password" required class="relative block w-full rounded-b-md border-0 py-1.5 text-gray-50 ring-1 ring-inset ring-gray-700 placeholder:text-gray-800 focus:z-10 focus:ring-2 focus:ring-inset focus:ring-indigo-500 sm:text-sm sm:leading-6" placeholder="Password">
              </div>
            </div>
      
            <div class="flex items-center justify-between">
              <div class="flex items-center">
                <input id="remember-me" name="remember-me" type="checkbox" class="h-4 w-4 rounded border-gray-700 text-indigo-500 focus:ring-indigo-500">
                <label for="remember-me" class="ml-2 block text-sm text-gray-50">Remember me</label>
              </div>
      
              <div class="text-sm">
                <a href="/register" class="font-medium text-indigo-500 hover:text-indigo-400">Doesn't have an account? Sign up</a>
              </div>
            </div>
      
            <div>
              <button type="submit" class="group relative flex w-full justify-center rounded-md bg-indigo-500 py-2 px-3 text-sm font-semibold text-white hover:bg-indigo-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500">
                <span class="absolute inset-y-0 left-0 flex items-center pl-3">
                  <svg class="h-5 w-5 text-indigo-400 group-hover:text-indigo-300" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                    <path fill-rule="evenodd" d="M10 1a4.5 4.5 0 00-4.5 4.5V9H5a2 2 0 00-2 2v6a2 2 0 002 2h10a2 2 0 002-2v-6a2 2 0 00-2-2h-.5V5.5A4.5 4.5 0 0010 1zm3 8V5.5a3 3 0 10-6 0V9h6z" clip-rule="evenodd" />
                  </svg>
                </span>
                Sign in
              </button>
            </div>
          </form>
        </div>
      </div>
  
