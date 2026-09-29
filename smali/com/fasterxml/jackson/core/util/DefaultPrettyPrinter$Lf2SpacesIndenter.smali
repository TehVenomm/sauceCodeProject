.class public Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;
.super Lcom/fasterxml/jackson/core/util/DefaultIndenter;
.source "DefaultPrettyPrinter.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Lf2SpacesIndenter"
.end annotation

.annotation runtime Ljava/lang/Deprecated;
.end annotation


# static fields
.field public static final instance:Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 419
    new-instance v0, Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;

    invoke-direct {v0}, Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;-><init>()V

    sput-object v0, Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;->instance:Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;

    return-void
.end method

.method public constructor <init>()V
    .locals 2
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const-string v0, "  "

    .line 425
    sget-object v1, Lcom/fasterxml/jackson/core/util/DefaultIndenter;->SYS_LF:Ljava/lang/String;

    invoke-direct {p0, v0, v1}, Lcom/fasterxml/jackson/core/util/DefaultIndenter;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;)V
    .locals 1
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const-string v0, "  "

    .line 432
    invoke-direct {p0, v0, p1}, Lcom/fasterxml/jackson/core/util/DefaultIndenter;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic withLinefeed(Ljava/lang/String;)Lcom/fasterxml/jackson/core/util/DefaultIndenter;
    .locals 0

    .line 412
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;->withLinefeed(Ljava/lang/String;)Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;

    move-result-object p1

    return-object p1
.end method

.method public withLinefeed(Ljava/lang/String;)Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;
    .locals 1

    .line 441
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;->getEol()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-object p0

    .line 444
    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;

    invoke-direct {v0, p1}, Lcom/fasterxml/jackson/core/util/DefaultPrettyPrinter$Lf2SpacesIndenter;-><init>(Ljava/lang/String;)V

    return-object v0
.end method
