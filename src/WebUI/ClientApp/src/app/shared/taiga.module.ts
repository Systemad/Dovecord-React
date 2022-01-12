import { NgModule } from '@angular/core';

import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TuiInputModule} from '@taiga-ui/kit';

import { CommonModule } from '@angular/common';
import {TuiAvatarModule, TuiTextAreaModule } from '@taiga-ui/kit';
import {
  TuiButtonModule,
  TuiCalendarModule,
  TuiDataListModule,
  TuiDialogModule,
  TuiDropdownControllerModule,
  TuiDropdownModule,
  TuiErrorModule,
  TuiExpandModule,
  TuiFormatNumberPipeModule,
  TuiFormatPhonePipeModule,
  TuiGroupModule,
  TuiHintControllerModule,
  TuiHintModule,
  TuiHostedDropdownModule,
  TuiLabelModule,
  TuiLinkModule,
  TuiLoaderModule,
  TuiManualHintModule,
  TuiModeModule,
  TuiNotificationModule,
  TuiNotificationsModule,
  TuiPointerHintModule,
  TuiPrimitiveCheckboxModule,
  TuiPrimitiveTextfieldModule,
  TuiRootModule,
  TuiScrollbarModule,
  TuiSvgModule,
  TuiSvgService,
  TuiTextfieldControllerModule,
  TuiTooltipModule,
} from '@taiga-ui/core';

const taigaModules = [

    CommonModule,
    TuiRootModule,
    TuiDialogModule,
    TuiNotificationsModule,
    TuiSvgModule,
    TuiHostedDropdownModule,
    TuiDataListModule,
    TuiButtonModule,
    TuiAvatarModule,
    FormsModule,
    ReactiveFormsModule,
    TuiInputModule,
    TuiModeModule,
    TuiTextAreaModule,
    TuiPrimitiveTextfieldModule,
    TuiDataListModule

]
@NgModule({
  declarations: [],
  imports: [
    ...taigaModules
  ],
  exports: [
    ...taigaModules
  ]
})
export class TaigaModule { }
