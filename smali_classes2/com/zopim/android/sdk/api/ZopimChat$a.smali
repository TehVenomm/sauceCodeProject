.class Lcom/zopim/android/sdk/api/ZopimChat$a;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/api/ZopimChat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "a"
.end annotation


# static fields
.field private static final a:Lcom/zopim/android/sdk/api/ZopimChat;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/api/ZopimChat;

    invoke-direct {v0}, Lcom/zopim/android/sdk/api/ZopimChat;-><init>()V

    sput-object v0, Lcom/zopim/android/sdk/api/ZopimChat$a;->a:Lcom/zopim/android/sdk/api/ZopimChat;

    return-void
.end method

.method static synthetic a()Lcom/zopim/android/sdk/api/ZopimChat;
    .locals 1

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat$a;->b()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v0

    return-object v0
.end method

.method private static b()Lcom/zopim/android/sdk/api/ZopimChat;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$a;->a:Lcom/zopim/android/sdk/api/ZopimChat;

    return-object v0
.end method
