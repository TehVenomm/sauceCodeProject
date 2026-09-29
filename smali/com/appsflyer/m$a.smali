.class final Lcom/appsflyer/m$a;
.super Ljava/lang/Object;
.source ""


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/m;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x18
    name = "a"
.end annotation


# instance fields
.field private final ˎ:Z

.field private final ˏ:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;Z)V
    .locals 0

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 27
    iput-object p1, p0, Lcom/appsflyer/m$a;->ˏ:Ljava/lang/String;

    .line 28
    iput-boolean p2, p0, Lcom/appsflyer/m$a;->ˎ:Z

    return-void
.end method


# virtual methods
.method final ˎ()Z
    .locals 1

    .line 36
    iget-boolean v0, p0, Lcom/appsflyer/m$a;->ˎ:Z

    return v0
.end method

.method public final ॱ()Ljava/lang/String;
    .locals 1

    .line 32
    iget-object v0, p0, Lcom/appsflyer/m$a;->ˏ:Ljava/lang/String;

    return-object v0
.end method
