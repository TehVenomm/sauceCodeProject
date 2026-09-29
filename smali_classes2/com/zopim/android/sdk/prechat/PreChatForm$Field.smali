.class public final enum Lcom/zopim/android/sdk/prechat/PreChatForm$Field;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/prechat/PreChatForm;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "Field"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/prechat/PreChatForm$Field;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

.field public static final enum NOT_REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

.field public static final enum OPTIONAL:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

.field public static final enum OPTIONAL_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

.field public static final enum REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

.field public static final enum REQUIRED_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;


# direct methods
.method static constructor <clinit>()V
    .locals 7

    new-instance v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    const-string v1, "NOT_REQUIRED"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->NOT_REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    new-instance v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    const-string v1, "OPTIONAL"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->OPTIONAL:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    new-instance v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    const-string v1, "REQUIRED"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    new-instance v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    const-string v1, "OPTIONAL_EDITABLE"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->OPTIONAL_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    new-instance v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    const-string v1, "REQUIRED_EDITABLE"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    const/4 v0, 0x5

    new-array v0, v0, [Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    sget-object v1, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->NOT_REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    aput-object v1, v0, v2

    sget-object v1, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->OPTIONAL:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->OPTIONAL_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    aput-object v1, v0, v6

    sput-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->$VALUES:[Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/prechat/PreChatForm$Field;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/prechat/PreChatForm$Field;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->$VALUES:[Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    return-object v0
.end method
