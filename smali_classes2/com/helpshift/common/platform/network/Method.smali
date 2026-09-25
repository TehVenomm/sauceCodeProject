.class public final enum Lcom/helpshift/common/platform/network/Method;
.super Ljava/lang/Enum;
.source "Method.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/common/platform/network/Method;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/common/platform/network/Method;

.field public static final enum GET:Lcom/helpshift/common/platform/network/Method;

.field public static final enum POST:Lcom/helpshift/common/platform/network/Method;

.field public static final enum PUT:Lcom/helpshift/common/platform/network/Method;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 4
    new-instance v0, Lcom/helpshift/common/platform/network/Method;

    const-string v1, "POST"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/platform/network/Method;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/platform/network/Method;->POST:Lcom/helpshift/common/platform/network/Method;

    .line 5
    new-instance v0, Lcom/helpshift/common/platform/network/Method;

    const-string v1, "GET"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/common/platform/network/Method;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/platform/network/Method;->GET:Lcom/helpshift/common/platform/network/Method;

    .line 6
    new-instance v0, Lcom/helpshift/common/platform/network/Method;

    const-string v1, "PUT"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/common/platform/network/Method;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/platform/network/Method;->PUT:Lcom/helpshift/common/platform/network/Method;

    const/4 v0, 0x3

    .line 3
    new-array v0, v0, [Lcom/helpshift/common/platform/network/Method;

    sget-object v1, Lcom/helpshift/common/platform/network/Method;->POST:Lcom/helpshift/common/platform/network/Method;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/common/platform/network/Method;->GET:Lcom/helpshift/common/platform/network/Method;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/common/platform/network/Method;->PUT:Lcom/helpshift/common/platform/network/Method;

    aput-object v1, v0, v4

    sput-object v0, Lcom/helpshift/common/platform/network/Method;->$VALUES:[Lcom/helpshift/common/platform/network/Method;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 3
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/common/platform/network/Method;
    .locals 1

    .line 3
    const-class v0, Lcom/helpshift/common/platform/network/Method;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/common/platform/network/Method;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/common/platform/network/Method;
    .locals 1

    .line 3
    sget-object v0, Lcom/helpshift/common/platform/network/Method;->$VALUES:[Lcom/helpshift/common/platform/network/Method;

    invoke-virtual {v0}, [Lcom/helpshift/common/platform/network/Method;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/common/platform/network/Method;

    return-object v0
.end method
