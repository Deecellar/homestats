import type { Load } from '@sveltejs/kit';
import { compile } from 'mdsvex';

const MOCK_RESPONSE_FROM_API =  `
En Sensoria Hogar nos dedicamos a proporcionar soluciones innovadoras y eficientes para hacer de tu hogar un lugar más seguro y cómodo. Nos especializamos en el diseño, fabricación y comercialización de **sensores inteligentes** que te permiten monitorear diferentes aspectos de tu hogar, como:

- Temperatura
- Humedad
- Exposición a la luz solar

Nuestros sensores utilizan tecnología de vanguardia para ofrecerte **información precisa y en tiempo real** sobre las condiciones de tu hogar, lo que te permite tomar decisiones informadas para mejorar tu bienestar y el de tu familia. Además, nuestros productos son **fáciles de instalar y usar**, y se integran perfectamente con otros dispositivos inteligentes de tu hogar, como tu asistente virtual o tu smartphone.

En Sensoria Hogar creemos que la tecnología puede hacer una gran diferencia en la forma en que vivimos, y estamos comprometidos en hacer de tu hogar un lugar más inteligente y seguro. ¡Conoce más sobre nuestros productos y servicios y descubre cómo podemos ayudarte a mejorar tu calidad de vida!
`


export const load: Load = async () => {
    const response = MOCK_RESPONSE_FROM_API; // Get data with eg. `fetch`
    const compiledResponse = await compile(response);

    console.log('compiledResponse is: ', compiledResponse);
    
    return { content: compiledResponse?.code };
};