import * as Cookies from 'es-cookie';
import jwt from 'jsonwebtoken';

const AUTH_COOKIE_KEY  = 'saneserver.auth.token';

export const AuthCookie = {
  get: () => {
    const token = Cookies.get(AUTH_COOKIE_KEY);
    if (token) {
      const decoded = jwt.decode(token);
      if (decoded && decoded.exp) {
        const tokenTTL = (new Date(decoded.exp * 1000).getTime() - new Date().getTime());
        if ( tokenTTL <= 300000) {
          Cookies.remove(AUTH_COOKIE_KEY);
          return false;
        } else {
          return {
            username: decoded.unique_name,
            role: decoded.role, 
            token: token,
            ttl: tokenTTL
          };
        }
      }      
    }
    return false;
  },
  set: (value, expiration) => {
    const expires = Math.round(
      (new Date(expiration).getTime() - new Date().getTime()) / (1000 * 3600 * 24)
    );
    Cookies.set(AUTH_COOKIE_KEY, value, { expires: expires });
  },
  remove: () => {
    Cookies.remove(AUTH_COOKIE_KEY);
  }
}