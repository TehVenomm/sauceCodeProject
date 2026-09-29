.class public final enum Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;
.super Ljava/lang/Enum;
.source "SourceFile"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gopay/vip/PurchaseEvent;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "VerificationStatus"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;",
        ">;"
    }
.end annotation


# static fields
.field public static final enum NOT_VERIFIED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

.field public static final enum VERIFICATION_FAILED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

.field public static final enum VERIFICATION_SUCCEEDED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

.field private static final synthetic b:[Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;


# instance fields
.field private final a:I


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 11
    new-instance v0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    const-string v1, "NOT_VERIFIED"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2, v2}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->NOT_VERIFIED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    .line 12
    new-instance v0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    const-string v1, "VERIFICATION_SUCCEEDED"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3, v3}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->VERIFICATION_SUCCEEDED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    .line 13
    new-instance v0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    const-string v1, "VERIFICATION_FAILED"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4, v4}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->VERIFICATION_FAILED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    const/4 v0, 0x3

    .line 10
    new-array v0, v0, [Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    sget-object v1, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->NOT_VERIFIED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    aput-object v1, v0, v2

    sget-object v1, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->VERIFICATION_SUCCEEDED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    aput-object v1, v0, v3

    sget-object v1, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->VERIFICATION_FAILED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    aput-object v1, v0, v4

    sput-object v0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->b:[Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;II)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)V"
        }
    .end annotation

    .line 17
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    .line 18
    iput p3, p0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->a:I

    return-void
.end method

.method public static fromValue(I)Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;
    .locals 5

    .line 26
    invoke-static {}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->values()[Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    .line 27
    invoke-virtual {v3}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->getValue()I

    move-result v4

    if-ne v4, p0, :cond_0

    return-object v3

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    const/4 p0, 0x0

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;
    .locals 1

    .line 10
    const-class v0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    return-object p0
.end method

.method public static values()[Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;
    .locals 1

    .line 10
    sget-object v0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->b:[Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    invoke-virtual {v0}, [Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    return-object v0
.end method


# virtual methods
.method public getValue()I
    .locals 1

    .line 22
    iget v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->a:I

    return v0
.end method
