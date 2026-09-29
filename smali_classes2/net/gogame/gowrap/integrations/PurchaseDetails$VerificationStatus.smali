.class public final enum Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;
.super Ljava/lang/Enum;
.source "PurchaseDetails.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/integrations/PurchaseDetails;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "VerificationStatus"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

.field public static final enum NOT_VERIFIED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

.field public static final enum VERIFICATION_FAILED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

.field public static final enum VERIFICATION_SUCCEEDED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 8
    new-instance v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    const-string v1, "NOT_VERIFIED"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->NOT_VERIFIED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    .line 9
    new-instance v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    const-string v1, "VERIFICATION_SUCCEEDED"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->VERIFICATION_SUCCEEDED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    .line 10
    new-instance v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    const-string v1, "VERIFICATION_FAILED"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->VERIFICATION_FAILED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    const/4 v0, 0x3

    .line 7
    new-array v0, v0, [Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    sget-object v1, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->NOT_VERIFIED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    aput-object v1, v0, v2

    sget-object v1, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->VERIFICATION_SUCCEEDED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    aput-object v1, v0, v3

    sget-object v1, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->VERIFICATION_FAILED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    aput-object v1, v0, v4

    sput-object v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->$VALUES:[Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 7
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;
    .locals 1

    .line 7
    const-class v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    return-object p0
.end method

.method public static values()[Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;
    .locals 1

    .line 7
    sget-object v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->$VALUES:[Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    invoke-virtual {v0}, [Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    return-object v0
.end method
