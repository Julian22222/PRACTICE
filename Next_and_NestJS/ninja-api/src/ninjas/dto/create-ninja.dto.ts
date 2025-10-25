// export class CreateNinjaDto {
//   name: string;
//   rank: string;
//   weapon: string;
// }

import { IsEnum, MinLength } from 'class-validator';

export class CreateNinjaDto {
  @MinLength(3)
  name: string;

  rank: string;

  @IsEnum(['stars', 'nunchucks'], { message: 'use correct weapon' })
  weapon: string;
}
