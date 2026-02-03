import { CanActivate, ExecutionContext, Injectable } from '@nestjs/common';
import { Observable } from 'rxjs';

@Injectable()
export class BeltGuard implements CanActivate {
  canActivate(
    context: ExecutionContext,
  ): boolean | Promise<boolean> | Observable<boolean> {
    //implement your belt checking logic here
    //can be used to get request object and check for headers, user roles, etc.
    // const request = context.switchToHttp().getRequest();  //parse the request out of the execution context
    //request includes various properties like headers, body, params, URL, cookies, etc. Then we can use that request object to implement our guard logic.

    // const headers = request.headers;
    // const userBelt = request.headers['belt']; //assuming belt info is sent in headers
    // if (userBelt !== 'black') {
    //   return false; //deny access to specific Routes, if belt is not black
    // }

    return true;
  }
}
