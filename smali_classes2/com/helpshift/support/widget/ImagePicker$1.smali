.class synthetic Lcom/helpshift/support/widget/ImagePicker$1;
.super Ljava/lang/Object;
.source "ImagePicker.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/widget/ImagePicker;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$helpshift$common$platform$Device$PermissionState:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 67
    invoke-static {}, Lcom/helpshift/common/platform/Device$PermissionState;->values()[Lcom/helpshift/common/platform/Device$PermissionState;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/helpshift/support/widget/ImagePicker$1;->$SwitchMap$com$helpshift$common$platform$Device$PermissionState:[I

    :try_start_0
    sget-object v0, Lcom/helpshift/support/widget/ImagePicker$1;->$SwitchMap$com$helpshift$common$platform$Device$PermissionState:[I

    sget-object v1, Lcom/helpshift/common/platform/Device$PermissionState;->AVAILABLE:Lcom/helpshift/common/platform/Device$PermissionState;

    invoke-virtual {v1}, Lcom/helpshift/common/platform/Device$PermissionState;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lcom/helpshift/support/widget/ImagePicker$1;->$SwitchMap$com$helpshift$common$platform$Device$PermissionState:[I

    sget-object v1, Lcom/helpshift/common/platform/Device$PermissionState;->UNAVAILABLE:Lcom/helpshift/common/platform/Device$PermissionState;

    invoke-virtual {v1}, Lcom/helpshift/common/platform/Device$PermissionState;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    :try_start_2
    sget-object v0, Lcom/helpshift/support/widget/ImagePicker$1;->$SwitchMap$com$helpshift$common$platform$Device$PermissionState:[I

    sget-object v1, Lcom/helpshift/common/platform/Device$PermissionState;->REQUESTABLE:Lcom/helpshift/common/platform/Device$PermissionState;

    invoke-virtual {v1}, Lcom/helpshift/common/platform/Device$PermissionState;->ordinal()I

    move-result v1

    const/4 v2, 0x3

    aput v2, v0, v1
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    return-void
.end method
