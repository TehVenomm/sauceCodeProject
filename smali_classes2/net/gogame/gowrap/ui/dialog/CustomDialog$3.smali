.class synthetic Lnet/gogame/gowrap/ui/dialog/CustomDialog$3;
.super Ljava/lang/Object;
.source "CustomDialog.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/dialog/CustomDialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$net$gogame$gowrap$ui$dialog$CustomDialog$Type:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 115
    invoke-static {}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->values()[Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$3;->$SwitchMap$net$gogame$gowrap$ui$dialog$CustomDialog$Type:[I

    :try_start_0
    sget-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$3;->$SwitchMap$net$gogame$gowrap$ui$dialog$CustomDialog$Type:[I

    sget-object v1, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->INFO:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$3;->$SwitchMap$net$gogame$gowrap$ui$dialog$CustomDialog$Type:[I

    sget-object v1, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ALERT:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    :try_start_2
    sget-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$3;->$SwitchMap$net$gogame$gowrap$ui$dialog$CustomDialog$Type:[I

    sget-object v1, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->PROGRESS:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x3

    aput v2, v0, v1
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    return-void
.end method
