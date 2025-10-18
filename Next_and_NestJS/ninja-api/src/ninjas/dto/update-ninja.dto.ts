import { PartialType } from '@nestjs/mapped-types';
import { CreateNinjaDto } from './create-ninja.dto';

//UpdateNinjaDto allows partial updates to ninja entities by extending CreateNinjaDto with optional properties.
//UpdateNinjaDto extends the CreateNinjaDto class, making all its properties optional for update operations.
// This is useful for scenarios where you want to update only certain fields of a ninja without needing to provide all the fields defined in CreateNinjaDto.
export class UpdateNinjaDto extends PartialType(CreateNinjaDto) {}
