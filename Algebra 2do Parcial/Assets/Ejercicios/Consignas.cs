/*
 * 1)   Vec1 gira el vector al rededor de los ejes
 * 
 * 2)   Vec1 gira al rededor de los ejes.
 *      Vec2 vertical (en el mismo sentido que el eje Y). Gira a 10 unidades de los ejes.
 *      Vec3 gira a 10 unidades de altura y distancia de los ejes.
 * 
 * 3)   Vec1 gira entre el 10,0,0 y el 0,10,0. Usa como eje 45° de rotacion del eje Z.
 *      Vec2 usa la punta del Vec1 como base y llega al 10,10,0.
 *      Vec3 gira entre el 20,10,0 y el 10,20,0. Usa como eje 45° de rotacion del eje Z.
 *      Vec4 usa la punta del Vec1 como base y llega al 20,20,0.
 *      
 *      
 *      Anotaciones: Si en vez de tomarlo como vectores, lo tomo puntos, hay 3 puntos que no se mueven
 *      0,0,0 - 10,10,0 - 20,20,0 no se mueven del lugar
 *      Por otra parte, el 2do punto, empezando de abajo, mirando en el mismo sentido que el eje X, gira en sentido antihorario
 *      Mientras que el 4to, gira en sentido horario.
 *      
 *      
*/