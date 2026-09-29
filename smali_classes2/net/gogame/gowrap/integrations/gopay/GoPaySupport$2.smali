.class synthetic Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$2;
.super Ljava/lang/Object;
.source "GoPaySupport.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$net$gogame$gowrap$integrations$PurchaseDetails$VerificationStatus:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 156
    invoke-static {}, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->values()[Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$2;->$SwitchMap$net$gogame$gowrap$integrations$PurchaseDetails$VerificationStatus:[I

    :try_start_0
    sget-object v0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$2;->$SwitchMap$net$gogame$gowrap$integrations$PurchaseDetails$VerificationStatus:[I

    sget-object v1, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->NOT_VERIFIED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    invoke-virtual {v1}, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$2;->$SwitchMap$net$gogame$gowrap$integrations$PurchaseDetails$VerificationStatus:[I

    sget-object v1, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->VERIFICATION_SUCCEEDED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    invoke-virtual {v1}, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    :try_start_2
    sget-object v0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$2;->$SwitchMap$net$gogame$gowrap$integrations$PurchaseDetails$VerificationStatus:[I

    sget-object v1, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->VERIFICATION_FAILED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    invoke-virtual {v1}, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->ordinal()I

    move-result v1

    const/4 v2, 0x3

    aput v2, v0, v1
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    return-void
.end method
